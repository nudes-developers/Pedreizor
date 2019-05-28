using Nudes.Pedreizor.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Nudes.Pedreizor
{
    public interface IPedreizor
    {
        PedreizorOptions Options { get; set; }

        /// <summary>
        /// Converts a html string to a pdf and stores it in a new stream
        /// </summary>
        /// <param name="htmlContent">Html string to convert</param>
        /// <returns>New stream containing pdf file</returns>
        Task<Stream> Pdfy(string htmlContent);

        /// <summary>
        /// Converts a razor to a pdf and stores it in a new stream
        /// </summary>
        /// <param name="razorPath">Razor to render path</param>
        /// <returns>New stream containing pdf file</returns>
        Task<Stream> Pdfy(Uri razorPath);

        /// <summary>
        /// Converts a razor to a pdf and stores it in a new stream
        /// </summary>
        /// <typeparam name="T">Type of razor model</typeparam>
        /// <param name="razorPath">Path of razor to render</param>
        /// <param name="data">Instance of razor model</param>
        /// <returns>New stream containing pdf file</returns>
        Task<Stream> Pdfy<T>(Uri razorPath, T data) where T : class;

        /// <summary>
        /// Converts a html string to a pdf and stores it in an existent stream
        /// </summary>
        /// <param name="htmlContent">Html string to convert</param>
        /// <param name="stream">Stream that will receive the data</param>
        /// <param name="streamOwner">Close the stream on complete</param>
        /// <returns></returns>
        Task PdfyTo(string htmlContent, Stream stream, bool streamOwner = false);

        /// <summary>
        /// Converts a razor to a pdf and stores it in an existent stream
        /// </summary>
        /// <param name="razorPath">Path of razor to render</param>
        /// <param name="stream">Stream that will receive the data</param>
        /// <param name="streamOwner">Close the stream on complete</param>
        /// <returns></returns>
        Task PdfyTo(Uri razorPath, Stream stream, bool streamOwner = false);

        /// <summary>
        /// Converts a razor to a pdf and stores it in an existent stream
        /// </summary>
        /// <typeparam name="T">Type of razor model</typeparam>
        /// <param name="razorPath">Path of razor to render</param>
        /// <param name="stream">Stream that will receive the data</param>
        /// <param name="data">Instance of razor model</param>
        /// <param name="streamOwner">Close the stream on complete</param>
        /// <returns></returns>
        Task PdfyTo<T>(Uri razorPath, Stream stream, T data, bool streamOwner = false) where T : class;
    }
}
