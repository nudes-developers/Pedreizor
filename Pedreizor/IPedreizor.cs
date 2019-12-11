using System.IO;
using System.Threading.Tasks;

namespace Nudes.Pedreizor
{
    public interface IPedreizor
    {
        /// <summary>
        /// Converts a html string to a pdf and stores it in a new stream
        /// </summary>
        /// <param name="htmlContent">Html string to convert</param>
        /// <returns>New stream containing pdf file</returns>
        Task<Stream> Pdfy(string htmlContent);

        /// <summary>
        /// Converts a html string to a pdf and stores it in an existent stream
        /// </summary>
        /// <param name="htmlContent">Html string to convert</param>
        /// <param name="stream">Stream that will receive the data</param>
        /// <param name="streamOwner">Close the stream on complete</param>
        /// <returns></returns>
        Task PdfyTo(string htmlContent, Stream stream, bool streamOwner = false);
    }
}
