using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Nudes.Pedreizor;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PedreizorFunction
{
    public class GeneratePdf
    {
        public GeneratePdf(IPedreizor pedreizor)
        {
            this.pedreizor = pedreizor;
        }

        private readonly IPedreizor pedreizor;
        private readonly IEnumerable<string> acceptedContentTypes = new List<string>
        {
            "text/plain", "text/html"
        };

        [FunctionName("GeneratePDF")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "generatePDF")]
                                                     HttpRequest request, ILogger logger)
        {
            var file = request.Form.Files.FirstOrDefault();

            if (file == null)
                return new BadRequestObjectResult("No file found for pdf generation");

            if (!acceptedContentTypes.Contains(file.ContentType))
                return new BadRequestObjectResult("Invalid ContentType");

            var content = await new StreamReader(file.OpenReadStream()).ReadToEndAsync();

            var memory = new MemoryStream();
            await pedreizor.PdfyTo(content, memory);

            return new FileContentResult(memory.ToArray(), "application/pdf; charset=utf-8");
        }
    }
}
