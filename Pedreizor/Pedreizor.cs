using DinkToPdf;
using DinkToPdf.Contracts;
using Nudes.Pedreizor.Configuration;
using Nudes.Pedreizor.Internal;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Nudes.Pedreizor
{
    public class Pedreizor : IPedreizor
    {
        private readonly IConverter converter;

        public Pedreizor() : this(new PedreizorOptions()) { }

        public Pedreizor(PedreizorOptions options)
        {
            PedreizorExtensions.LoadWebkitLibrary();
            this.converter = ConverterContainer.Instance;
            this.Options = options;
        }

        public PedreizorOptions Options { get; set; }

        public async Task<Stream> Pdfy(string htmlContent)
        {
            var stream = new MemoryStream();
            await this.PdfyTo(htmlContent, stream);

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

            if (streamOwner) stream.Close();
        }

        private ObjectSettings GeneratePdfObjectSettings(string htmlContent)
        {
            var settings = new ObjectSettings
            {
                HtmlContent = htmlContent,
                WebSettings = new WebSettings
                {
                    DefaultEncoding = Encoding.UTF8.WebName,
                    EnableJavascript = false,
                },
                UseLocalLinks = false,
            };

            if (Options.PageCounterVisible)
                switch (Options.PageCounterPosition)
                {
                    case PageNumberPosition.FooterLeft:
                        settings.FooterSettings = new FooterSettings
                        {
                            Left = "[page]"
                        };
                        break;
                    case PageNumberPosition.FooterCenter:
                        settings.FooterSettings = new FooterSettings
                        {
                            Center = "[page]"
                        };
                        break;
                    case PageNumberPosition.FooterRight:
                        settings.FooterSettings = new FooterSettings
                        {
                            Right = "[page]",
                        };
                        break;
                    case PageNumberPosition.HeaderLeft:
                        settings.HeaderSettings = new HeaderSettings
                        {
                            Left = "[page]"
                        };
                        break;

                    case PageNumberPosition.HeaderCenter:
                        settings.HeaderSettings = new HeaderSettings
                        {
                            Center = "[page]"
                        };
                        break;
                    case PageNumberPosition.HeaderRight:
                        settings.HeaderSettings = new HeaderSettings
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
