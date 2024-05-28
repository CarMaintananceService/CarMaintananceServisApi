using Business;
using Business.Shared;

namespace Api.Middlewares
{
    public class HttpContextMiddleware
    {
        private readonly RequestDelegate _next;

        public HttpContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task InvokeAsync(HttpContext context)
        {

            var dataService = context.RequestServices.GetRequiredService<CurrentServiceProvider>();
            if (dataService != null)
            {
                if (dataService != null)
                    dataService.Set(context.RequestServices);
            }

            return _next(context);
        }

    }
}
