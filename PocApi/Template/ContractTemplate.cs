using Nudes.Pedreizor;
using Nudes.Pedreizor.Configuration;
using Nudes.Pedreizor.RazorRenderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PocApi.Template
{
    public class ContractTemplate : Pedreizor
    {
        public override string Title { get => base.Title; set => base.Title = $"{DateTime.Now} - {value}"; }

        public override Paper Paper => PapersType.A5;

        public override bool PageCounterVisible => false;        

        public ContractTemplate(IRazorRenderer razorRenderer) : base(razorRenderer) { }
    }
}
