using Microsoft.AspNetCore.Mvc;
using Nudes.Pedreizor;
using Nudes.Pedreizor.Configuration;
using Nudes.Pedreizor.RazorRenderer;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PocApi.Controllers
{
    [Route("api/[controller]")]
    public class PocController : Controller
    {
        private readonly IPedreizor pedreizor;
        private readonly IPedreizor pedreizor2;

        public PocController(IRazorRenderer razorRenderer)
        {
            this.pedreizor = new Pedreizor(razorRenderer)
            {
                Title = "Title",
                Paper = PapersType.A4Landscape,
                PageCounterVisible = true,
                PageCounterPosition = PageNumberPosition.Center
            };
            this.pedreizor2 = new Pedreizor(razorRenderer)
            {
                Title = "Title",
                Paper = PapersType.A4,
                PageCounterVisible = true,
                PageCounterPosition = PageNumberPosition.Center
            };
        }

        public async Task<IActionResult> Get()
        {
            var mem = new MemoryStream();

            await pedreizor.PdfyTo(new Uri("/Controllers/Index.cshtml", UriKind.Relative), mem, new TestModel
            {
                Name = "Teste GET 1"
            });
            
            return new FileContentResult(mem.ToArray(), "application/pdf");
        }

        [Route("2")]
        public async Task<IActionResult> Get2()
        {
            var mem = new MemoryStream();

            await pedreizor2.PdfyTo(new Uri("/Controllers/Index.cshtml", UriKind.Relative), mem, new TestModel
            {
                Name = "Teste GET 2"
            });

            return new FileContentResult(mem.ToArray(), "application/pdf");
        }
    }
}
