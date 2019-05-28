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
        private readonly IRazorRenderer razorRenderer;
        private readonly IPedreizor pedreizor;

        public PocController(IPedreizor pedreizor)
        {
            this.pedreizor = pedreizor;
        }

        public async Task<IActionResult> Get()
        {
            MemoryStream stream = new MemoryStream();

            await pedreizor.PdfyTo(new Uri("/Template/Index.cshtml", UriKind.Relative), stream);

            return new FileContentResult(stream.ToArray(), "application/pdf");
        }

        //[Route("2")]
        //public async Task<IActionResult> Get2()
        //{
        //    var mem = new MemoryStream();

        //    await pedreizorOriginal.PdfyTo(new Uri("/Controllers/Index.cshtml", UriKind.Relative), mem);

        //    return new FileContentResult(mem.ToArray(), "application/pdf");
        //}

        //[Route("3")]
        //public async Task<IActionResult> Get3()
        //{
        //    var mem = new MemoryStream();

        //    await pedreizorOriginal.PdfyTo<TestModel>(new Uri("/Controllers/Index.cshtml", UriKind.Relative), mem, new TestModel
        //    {
        //        Name = "Teste"
        //    });

        //    return new FileContentResult(mem.ToArray(), "application/pdf");
        //}
    }
}
