using Microsoft.AspNetCore.Mvc;
using Nudes.Pedreizor;
using Nudes.Pedreizor.Configuration;
using Nudes.Pedreizor.RazorRenderer;
using PocApi.Template;
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
        private readonly IPedreizor pedreizorOriginal;
        private readonly IPedreizor contractTemplate;
        private readonly IPedreizor contractTemplate2;

        public PocController(IRazorRenderer razorRenderer, IPedreizor pedreizor, ContractTemplate contractTemplate, ContractTemplate2 contractTemplate2)
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
            pedreizorOriginal = pedreizor;
            this.contractTemplate = contractTemplate;
            this.contractTemplate2 = contractTemplate2;
        }

        public async Task<IActionResult> Get()
        {
            var mem = new MemoryStream();

            await contractTemplate.PdfyTo(new Uri("/Controllers/Index.cshtml", UriKind.Relative), mem, new TestModel
            {
                Name = "Teste GET 1"
            });
            
            return new FileContentResult(mem.ToArray(), "application/pdf");
        }

        [Route("2")]
        public async Task<IActionResult> Get2()
        {
            var mem = new MemoryStream();

            await contractTemplate2.PdfyTo(new Uri("/Controllers/Index.cshtml", UriKind.Relative), mem, new TestModel
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
