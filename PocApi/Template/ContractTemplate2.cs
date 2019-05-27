using Nudes.Pedreizor;
using Nudes.Pedreizor.Configuration;
using Nudes.Pedreizor.RazorRenderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PocApi.Template
{
    public class ContractTemplate2 : Pedreizor
    {
        public override string Title { get => base.Title; set => base.Title = $"{DateTime.Now} - {value} - v2"; }

        public override Paper Paper => PapersType.A5Landscape;

        public override bool PageCounterVisible => true;

        public ContractTemplate2(IRazorRenderer razorRenderer) : base(razorRenderer) { }
    }
}
