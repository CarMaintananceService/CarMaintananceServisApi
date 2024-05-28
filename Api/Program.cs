using Api.Lib;
using Api.Middlewares;
using Business;
using Business.Security.Users;
using Business.Shared;
using Core.Shared.Entities;
using EntityFrameworkCore.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using Web.Middlewares;
using Hangfire;
using Api.ScheduledServices;
using Microsoft.OpenApi.Models;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Business.App.CaseTypes;

var builder = WebApplication.CreateBuilder(args);
string AllowSpecificOrigins = "AllowSpecificOrigins";

// Add services to the container.
//builder.Services.AddControllersWithViews();

builder.Services.AddScoped(typeof(UserDataProvider), typeof(UserDataProvider));

string DefaultConnectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");

builder.Services.AddSingleton<AppInfo>(ap => builder.Configuration.GetSection("AppInfo").Get<AppInfo>());

builder.Services.AddPooledDbContextFactory<ApplicationDbContext>(
        options => options.UseSqlServer(DefaultConnectionString));

builder.Services.AddDbContextPool<ApplicationDbContext>(options =>
{
    options.UseSqlServer(DefaultConnectionString);

});
builder.Services.AddScoped<DbContext, ApplicationDbContext>();

builder.Services.AddAutoMapper(CustomDtoMapper.CreateMappings, AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSingleton<IMemoryCache, MemoryCache>();


builder.Services.AddTransient(typeof(IRepository<>), typeof(RepositoryBase<>));
builder.Services.AddScoped<CurrentServiceProvider>();
builder.Services.AddHttpContextAccessor();



#region Engines


builder.Services.AddScoped(typeof(JwtEngineUser), typeof(JwtEngineUser));
builder.Services.AddScoped(typeof(CaseTypeEngine), typeof(CaseTypeEngine));

#endregion

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(name: hrWebGateAllowSpecificOrigins,
//        sb =>
//        {
//            sb.WithOrigins(builder.Configuration["App:CorsOrigins"]
//                    .Split(",", StringSplitOptions.RemoveEmptyEntries)
//                    .ToArray())
//                    .SetIsOriginAllowedToAllowWildcardSubdomains()
//                    .AllowAnyHeader()
//                    .AllowAnyMethod()
//                    .AllowCredentials();
//        });
//});

builder.Services.AddHangfire(config =>
{
    config.UseSqlServerStorage(DefaultConnectionString).WithJobExpirationTimeout(TimeSpan.FromHours(6));
});
builder.Services.AddHangfireServer();




builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = null;
    //options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
    options.JsonSerializerOptions.NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString;
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "SuAtm", Version = $"{AppInfo.VersionNumber}.{AppInfo.VersionNumberMinor}" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
    {
        new OpenApiSecurityScheme
        {
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
        },
        new string[] { }
    }
    });
});



builder.Services.Configure<TokenOptions>(builder.Configuration.GetSection("TokenOptions"));
var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(jwtbeareroptions =>
{
    jwtbeareroptions.SaveToken = true;
    jwtbeareroptions.RequireHttpsMetadata = false;
    jwtbeareroptions.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        //ValidateLifetime = true,
        //ValidateIssuerSigningKey = true,
        ValidIssuer = tokenOptions.Issuer,
        ValidAudience = tokenOptions.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(tokenOptions.SecurityKey)),
        ClockSkew = TimeSpan.Zero
    };

    string message = "";

    jwtbeareroptions.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = ctx =>
        {
            ctx.Response.StatusCode = StatusCodes.Status401Unauthorized;
            message += "From OnAuthenticationFailed:\n";
            message += ctx.Exception;
            return Task.CompletedTask;
        },

        //OnChallenge = ctx =>
        //{
        //    message += "From OnChallenge:\n";
        //    ctx.Response.StatusCode = StatusCodes.Status401Unauthorized;
        //    ctx.Response.ContentType = "text/plain";
        //    return ctx.Response.WriteAsync(message);

        //},

        OnMessageReceived = ctx =>
        {
            message = "From OnMessageReceived:\n";
            ctx.Request.Headers.TryGetValue("Authorization", out var BearerToken);
            if (BearerToken.Count == 0)
                BearerToken = "no Bearer token sent\n";
            message += "Authorization Header sent: " + BearerToken + "\n";
            return Task.CompletedTask;
        },

        OnTokenValidated = ctx =>
        {
            var dataService = ctx.HttpContext.RequestServices.GetRequiredService<UserDataProvider>();
            if (dataService != null)
            {
                if (dataService != null)
                    dataService.Set(ctx.Principal);
            }

            //Debug.WriteLine("token: " + ctx.SecurityToken.ToString());
            return Task.CompletedTask;
        }
    };

});






var app = builder.Build();


app.UseDeveloperExceptionPage();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseMiddleware<HttpContextMiddleware>();
app.UseMiddleware<SetAcceptHeaderMiddleware>();

//app.UseResponseCompression();

//app.UseCors(AllowSpecificOrigins);





app.UseCors(x => x
.AllowAnyOrigin()
.AllowAnyMethod()
.AllowAnyHeader()
);

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SuAtm");
    c.RoutePrefix = "";
});

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
