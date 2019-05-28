using Nudes.Pedreizor.Configuration;

namespace Nudes.Pedreizor
{
    public class PedreizorOptions
    {
        public virtual string Title { get; set; } = string.Empty;
        public virtual Paper Paper { get; set; } = PaperType.A4;
        public virtual bool PageCounterVisible { get; set; } = false;
        public virtual PageNumberPosition PageCounterPosition { get; set; } = PageNumberPosition.Left;
    }
}