namespace Nudes.Pedreizor.Configuration
{
    public struct Paper
    {
        /// <summary>
        /// Paper width in mm
        /// </summary>
        public float Width { get; set; }

        /// <summary>
        /// Paper height in mm
        /// </summary>
        public float Height { get; set; }

        /// <summary>
        /// Paper margin
        /// </summary>
        public Margin Margin { get; set; }


        /// <summary>
        /// Return a new paper with width and heigth inversed
        /// </summary>
        /// <returns></returns>
        public Paper GetInversed() => new Paper()
        {
            Height = this.Width,
            Width = this.Height,
            Margin = this.Margin.RotateRight(),
        };
    }
}
