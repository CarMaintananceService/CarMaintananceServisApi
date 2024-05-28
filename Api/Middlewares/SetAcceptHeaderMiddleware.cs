using Azure.Core;

namespace Web.Middlewares
{
	public class SetAcceptHeaderMiddleware
	{
		private readonly RequestDelegate _next;

		public SetAcceptHeaderMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public Task InvokeAsync(HttpContext context)
		{
			//if (!Startup.appSettings.SetAcceptHeader)
			//    return _next(context);

			context.Request.Headers["Accept-Language"] = "tr-TR";
			/*
			context.Request.Headers["Accept-Language"] = context.Request.Headers["Accept-Language"].ToString().Split(";").FirstOrDefault()?.Split(",").FirstOrDefault();

			if (context.Request.RouteValues.ContainsKey("area"))
			{
				context.Request.Headers["Accept-Language"] = context.Request.RouteValues.GetValueOrDefault("area").ToString();
			}

			context.Request.Headers["Accept-Language"] = "en-US";

			string domain = context.Request.Host.Value;
			string[] segments = domain.Split('.');
			string subdomain = "";

			if (segments.Length >= 3)
			{
				subdomain = segments[0];
				if (subdomain != "www")
				{
					context.Request.Headers["Accept-Language"] = subdomain;
				}
			}
			*/
			return _next(context);
		}

	}
}
