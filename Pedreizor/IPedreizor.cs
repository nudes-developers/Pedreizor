using Pedreizor.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Pedreizor
{
    public interface IPedreizor
    {
        string Title { get; set; }

        Paper Paper { get; set; }

        bool PageCounterVisible { get; set; }

        PageNumberPosition PageCounterPosition { get; set; }

        Task<Stream> Pdfy(string htmlContent);
        Task<Stream> Pdfy(Uri razorPath);
        Task<Stream> Pdfy<T>(Uri razorPath, T data) where T : class;

        Task PdfyTo(string htmlContent, Stream stream, bool streamOwner = false);
        Task PdfyTo(Uri razorPath, Stream stream, bool streamOwner = false);
        Task PdfyTo<T>(Uri razorPath, Stream stream, T data, bool streamOwner = false) where T : class;
    }
}
