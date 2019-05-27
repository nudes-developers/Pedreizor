using DinkToPdf;
using DinkToPdf.Contracts;
using System;

namespace Pedreizor.Internal
{
    public sealed class ConverterContainer
    {
        private ConverterContainer() { }

        private static readonly Lazy<IConverter> lazy = new Lazy<IConverter>(() => new SynchronizedConverter(new PdfTools()));

        public static IConverter Instance => lazy.Value;
    }
}
