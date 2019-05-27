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

        /// <summary>
        /// Top margin
        /// </summary>
        public int Top { get; set; }

        /// <summary>
        /// Left margin
        /// </summary>
        public int Left { get; set; }

        /// <summary>
        /// Bottom margin
        /// </summary>
        public int Bottom { get; set; }

        /// <summary>
        /// Right margin
        /// </summary>
        public int Right { get; set; }

        /// <summary>
        /// Rotare margins to right
        /// </summary>
        public Margin RotateRight() => new Margin
        {
            Left = this.Bottom,
            Top = this.Left,
            Right = this.Top,
            Bottom = this.Right
        };

        /// <summary>
        /// Rotare margins to left
        /// </summary>
        public Margin RotateLeft() => new Margin
        {
            Bottom = this.Left,
            Left = this.Top,
            Top = this.Right,
            Right = this.Bottom
        };
    }
}
