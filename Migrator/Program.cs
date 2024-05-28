using EntityFrameworkCore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Migrator.Seed;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

Task.Run(async () =>
{

    string? answer = "";

    do
    {
        Console.Clear();
        Console.Write("Do you want to apply your migration? [y/n] : ");
        answer = Console.ReadLine().ToLower();
    }
    while (!(answer == "y" || answer == "n"));

    if (answer == "y")
    {
        IConfiguration configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .Build();

        string defaultConnectionString = configuration.GetConnectionString("Default");

        var serviceProvider = new ServiceCollection()
            .AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(defaultConnectionString))
            .AddTransient(typeof(DbContext), typeof(ApplicationDbContext))
            .BuildServiceProvider();

        var dbcontext = serviceProvider.GetService<ApplicationDbContext>();

        // Load configuration
        //var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
        //XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

        //ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        //logger.Error("osman errorü oluştu");

        //migrationBuilder.AlterDatabase("CollationName");
        await dbcontext.Database.MigrateAsync();
        
        //dbcontext.Database.ExecuteSqlRaw("CREATE FULLTEXT CATALOG {0} AS DEFAULT","osman");
        //await new DefaultLanguagesCreator(dbcontext).CreateAsync();
        await new DefaultUsersCreator(dbcontext).CreateAsync();
        Console.WriteLine("Migration is done!");
    }
    else
    {
        Console.WriteLine("Migration is not done!");
    }

    Console.ReadKey();

}).Wait();
