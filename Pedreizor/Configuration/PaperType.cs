namespace Nudes.Pedreizor.Configuration
{
    public static class PaperType
    {
        /// <summary>
        /// Paper with 420 x 594 mm and margin of 10mm
        /// </summary>
        public static Paper A2 => new Paper { Width = 420, Height = 594, Margin = new Margin(10) };

        /// <summary>
        /// Paper with 297 x 420 mm and margin of 10mm
        /// </summary>
        public static Paper A3 => new Paper { Width = 297, Height = 420, Margin = new Margin(10) };

        /// <summary>
        /// Paper with 210 x 297 mm and margin of 10mm
        /// </summary>
        public static Paper A4 => new Paper { Width = 210, Height = 297, Margin = new Margin(10) };

        /// <summary>
        /// Paper with 148 x 210 mm and margin of 10mm
        /// </summary>
        public static Paper A5 => new Paper { Width = 148, Height = 210, Margin = new Margin(10) };

        /// <summary>
        /// Paper with 105 x 148 mm and margin of 10mm
        /// </summary>
        public static Paper A6 => new Paper { Width = 105, Height = 148, Margin = new Margin(10) };

        /// <summary>
        /// Landscape A2 Paper
        /// </summary>
        public static Paper A2Landscape => A2.GetInversed();

        /// <summary>
        /// Landscape A3 Paper
        /// </summary>
        public static Paper A3Landscape => A3.GetInversed();

        /// <summary>
        /// Landscape A4 Paper
        /// </summary>
        public static Paper A4Landscape => A4.GetInversed();

        /// <summary>
        /// Landscape A5 Paper
        /// </summary>
        public static Paper A5Landscape => A5.GetInversed();

        /// <summary>
        /// Landscape A6 Paper
        /// </summary>
        public static Paper A6Landscape => A6.GetInversed();
    }
}
