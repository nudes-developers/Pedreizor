using DinkToPdf;
using DinkToPdf.Contracts;
using System;

namespace Nudes.Pedreizor.Internal
{
    internal sealed class ConverterContainer
    {
        private ConverterContainer() { }

        private static readonly Lazy<IConverter> converterInstance = new Lazy<IConverter>(() => new SynchronizedConverter(new PdfTools()));

        private static readonly Lazy<bool> librariesAlreadyLoadedInstance = new Lazy<bool>(() => false);

        public static IConverter Instance => converterInstance.Value;

        public static bool LibrariesAlreadyLoaded { get; internal set; } = librariesAlreadyLoadedInstance.Value;
    }
}
