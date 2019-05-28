using DinkToPdf;
using DinkToPdf.Contracts;
using Nudes.Pedreizor.Configuration;
using Nudes.Pedreizor.Internal;
using Nudes.Pedreizor.RazorRenderer;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Nudes.Pedreizor
{
    public class Pedreizor : IPedreizor
    {
        private readonly IConverter converter;
        private readonly IRazorRenderer razorRenderer;

        public Pedreizor(IRazorRenderer razorRenderer, PedreizorOptions options)
        {
            this.converter = ConverterContainer.Instance;
            this.razorRenderer = razorRenderer;
            this.Options = options;
        }

        public PedreizorOptions Options { get; set; }
        
        public async Task<Stream> Pdfy(string htmlContent)
        {
            var stream = new MemoryStream();
            await this.PdfyTo(htmlContent, stream);

            return stream;
        }

        public async Task<Stream> Pdfy(Uri razorPath)
        {
            var stream = new MemoryStream();
            await this.PdfyTo(razorPath, stream);

            return stream;
        }

        public async Task<Stream> Pdfy<T>(Uri razorPath, T dataModel) where T : class
        {
            var stream = new MemoryStream();
            await this.PdfyTo<T>(razorPath, stream, dataModel);

            return stream;
        }

        public async Task PdfyTo(string htmlContent, Stream stream, bool streamOwner = false)
        {
            var bytes = await Task.Run(() => converter.Convert(new HtmlToPdfDocument
            {
                GlobalSettings = new GlobalSettings
                {
                    DocumentTitle = Options.Title,
                    PaperSize = new PechkinPaperSize(Options.Paper.Width.ToString(), Options.Paper.Height.ToString()),
                    Margins = new MarginSettings(Options.Paper.Margin.Top, Options.Paper.Margin.Right, Options.Paper.Margin.Bottom, Options.Paper.Margin.Left),
                },
                Objects = {
                   this.GeneratePdfObjectSettings(htmlContent)
                }
            }));

            await stream.WriteAsync(bytes, 0, bytes.Length);
            await stream.FlushAsync();

            if (streamOwner)
                stream.Close();
        }

        public async Task PdfyTo(Uri razorPath, Stream stream, bool streamOwner = false)
        {
            var htmlContent = await razorRenderer.Render(razorPath);

            await this.PdfyTo(htmlContent, stream, streamOwner);
        }

        public async Task PdfyTo<T>(Uri razorPath, Stream stream, T dataModel, bool streamOwner = false) where T : class
        {
            var htmlContent = await razorRenderer.Render<T>(razorPath, dataModel);

            await this.PdfyTo(htmlContent, stream, streamOwner);
        }

        private ObjectSettings GeneratePdfObjectSettings(string htmlContent)
        {
            var settings = new ObjectSettings
            {
                HtmlContent = htmlContent
            };

            if (Options.PageCounterVisible)
                switch (Options.PageCounterPosition)
                {
                    case PageNumberPosition.Left:
                        settings.FooterSettings = new FooterSettings
                        {
                            Left = "[page]"
                        };
                        break;
                    case PageNumberPosition.Center:
                        settings.FooterSettings = new FooterSettings
                        {
                            Center = "[page]"
                        };
                        break;
                    case PageNumberPosition.Right:
                        settings.FooterSettings = new FooterSettings
                        {
                            Right = "[page]"
                        };
                        break;
                    default:
                        break;
                }

            return settings;
        }
    }
}
