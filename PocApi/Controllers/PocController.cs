using Microsoft.AspNetCore.Mvc;
using Nudes.Pedreizor;
using Nudes.Pedreizor.RazorRenderer;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PocApi.Controllers
{
    [Route("api/[controller]")]
    public class PocController : Controller
    {
        private readonly IPedreizor pedreizorOriginal;

        public PocController(IRazorRenderer razorRenderer, IPedreizor pedreizor)
        {
            pedreizorOriginal = pedreizor;
        }

        public async Task<IActionResult> Get()
        {
            var mem = new MemoryStream();

            await pedreizorOriginal.PdfyTo(new Uri("/Controllers/Index.cshtml", UriKind.Relative), mem, new TestModel
            {
                Name = "Teste GET 1"
            });
            
            return new FileContentResult(mem.ToArray(), "application/pdf");
        }

        [Route("2")]
        public async Task<IActionResult> Get2()
        {
            var mem = new MemoryStream();

            await pedreizorOriginal.PdfyTo(new Uri("/Controllers/Index.cshtml", UriKind.Relative), mem, new TestModel
            {
                Name = "Teste GET 2"
            });

            return new FileContentResult(mem.ToArray(), "application/pdf");
        }

        [Route("3")]
        public async Task<IActionResult> Get3()
        {
            var mem = new MemoryStream();

            await pedreizorOriginal.PdfyTo(new Uri("/Controllers/Index.cshtml", UriKind.Relative), mem);

            return new FileContentResult(mem.ToArray(), "application/pdf");
        }
    }
}
