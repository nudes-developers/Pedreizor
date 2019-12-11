using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Nudes.Pedreizor;
using Nudes.RazorRenderer;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PocApi.Controllers
{
    [Route("api/[controller]")]
    public class PocController : Controller
    {
        private readonly IRazorRenderer razorRenderer;
        private readonly string azureFunctionUri;
        private readonly IPedreizor pedreizor;

        public PocController(IPedreizor pedreizor, IRazorRenderer razorRenderer, IConfiguration configuration)
        {
            this.azureFunctionUri = configuration["PdfFunction:Uri"];
            this.pedreizor = pedreizor;
            this.razorRenderer = razorRenderer;
        }

        [HttpGet("1")]
        public async Task<IActionResult> RenderTemplateAndPdfyWithPedreizorToMemoryStreamAlreadyExistent()
        {
            MemoryStream stream = new MemoryStream();

            var htmlText = await razorRenderer.Render(new Uri("/Template/Index.cshtml", UriKind.Relative));

            await pedreizor.PdfyTo(htmlText, stream);

            return new FileContentResult(stream.ToArray(), "application/pdf");
        }

        [HttpGet("2")]
        public async Task<IActionResult> RenderTemplateAndPdfyWithPedreizorToStreamReturn()
        {
            var htmlText = await razorRenderer.Render(new Uri("/Template/Index.cshtml", UriKind.Relative));

            var stream = await pedreizor.Pdfy(htmlText);

            var memory = new MemoryStream();
            await stream.CopyToAsync(memory);

            return new FileContentResult(memory.ToArray(), "application/pdf");
        }

        [HttpGet("3")]
        public async Task<IActionResult> RenderTemplateAndPdfyWithAzureFunctions()
        {
            var htmlText = await razorRenderer.Render(new Uri("/Template/Index.cshtml", UriKind.Relative));

            var contentStream = new StreamContent(new MemoryStream(Encoding.UTF8.GetBytes(htmlText)));
            contentStream.Headers.ContentType = new MediaTypeHeaderValue("text/html");

            using var client = new HttpClient();
            using var content = new MultipartFormDataContent
            {
                { contentStream, "file", "file.html" }
            };
            using var response = await client.PostAsync($"{azureFunctionUri}", content);

            if (!response.IsSuccessStatusCode) return BadRequest();

            var bytes = await response.Content.ReadAsByteArrayAsync();

            return new FileContentResult(bytes, "application/pdf");
        }
    }
}
