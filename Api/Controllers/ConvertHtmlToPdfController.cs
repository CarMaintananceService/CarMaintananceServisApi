using iText.Html2pdf;
using iText.Html2pdf.Resolver.Font;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConvertHtmlToPdfController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public ConvertHtmlToPdfController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpPost("convert")]
        public IActionResult ConvertHtmlToPdf()
        {
           

            string htmlFilePath = Path.Combine(_env.WebRootPath, "Schema/Makbuz/index.html");

            if (!System.IO.File.Exists(htmlFilePath))
            {
                return NotFound("HTML file not found.");
            }

            try
            {
                string htmlContent = System.IO.File.ReadAllText(htmlFilePath);

                htmlContent = htmlContent.Replace("{AdSoyad}", "Özkan DANACI");
                htmlContent = htmlContent.Replace("{Ton}", "10");
                htmlContent = htmlContent.Replace("{Tutar}", "100");

                using (MemoryStream pdfStream = new MemoryStream())
                {
                    ConverterProperties converterProperties = new ConverterProperties();
                    converterProperties.SetFontProvider(new DefaultFontProvider(true, true, true));
                    converterProperties.SetBaseUri(_env.WebRootPath + "\\Schema\\Makbuz");
                    HtmlConverter.ConvertToPdf(htmlContent, pdfStream, converterProperties);
                    return File(pdfStream.ToArray(), "application/pdf", "output.pdf");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }



    public class HtmlToPdfRequest
    {
        public string HtmlFilePath { get; set; }
        public string BaseUri { get; set; }
    }
}
