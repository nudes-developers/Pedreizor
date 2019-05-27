namespace Pedreizor.Configuration
{
    public struct Paper
    {
        public float Width { get; set; }

        public float Height { get; set; }

        public Margin Margin { get; set; }

        public Paper GetInversed() => new Paper()
        {
            Height = this.Width,
            Width = this.Height,
            Margin = this.Margin.RotateRight(),
        };
    }
}
