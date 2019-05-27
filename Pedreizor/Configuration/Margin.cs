namespace Nudes.Pedreizor.Configuration
{
    public struct Margin
    {
        public Margin(int vertical, int horizontal)
        {
            Right = Left = horizontal;
            Top = Bottom = vertical;
        }

        public Margin(int size)
        {
            Top = Bottom = Left = Right = size;
        }

        public int Top { get; set; }
        public int Left { get; set; }
        public int Bottom { get; set; }
        public int Right { get; set; }

        public Margin RotateRight() => new Margin
        {
            Left = this.Bottom,
            Top = this.Left,
            Right = this.Top,
            Bottom = this.Right
        };

        public Margin RotateLeft() => new Margin
        {
            Bottom = this.Left,
            Left = this.Top,
            Top = this.Right,
            Right = this.Bottom
        };
    }
}
