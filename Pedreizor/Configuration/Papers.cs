namespace Pedreizor.Configuration
{
    public static class PapersType
    {
        public static Paper A2 => new Paper { Width = 420, Height = 594, Margin = new Margin(10) };
        public static Paper A3 => new Paper { Width = 297, Height = 420, Margin = new Margin(10) };
        public static Paper A4 => new Paper { Width = 210, Height = 297, Margin = new Margin(10) };
        public static Paper A5 => new Paper { Width = 148, Height = 210, Margin = new Margin(10) };
        public static Paper A6 => new Paper { Width = 105, Height = 148, Margin = new Margin(10) };

        public static Paper A2Landscape => A2.GetInversed();
        public static Paper A3Landscape => A3.GetInversed();
        public static Paper A4Landscape => A4.GetInversed();
        public static Paper A5Landscape => A5.GetInversed();
        public static Paper A6Landscape => A6.GetInversed();
    }
}
