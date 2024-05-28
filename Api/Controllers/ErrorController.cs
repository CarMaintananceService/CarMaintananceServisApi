using Business.Shared;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{

    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        [Route("error")]
        public IActionResult Error()
        {
            var exception = Request.HttpContext.Features.Get<IExceptionHandlerPathFeature>().Error;

            //loglayacağız ---->
            //userId
            //HttpContext.TraceIdentifier
            //HttpContext.Request.Path
            //exception.Message
            //exception.InnerException?.Message
            //------>

            return StatusCode(200, new TResponse<object>()
            {
                Error = exception.Message,
                Success = false
            });
        }

    }
}
