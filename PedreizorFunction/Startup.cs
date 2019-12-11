using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Nudes.Pedreizor;

[assembly: FunctionsStartup(typeof(PedreizorFunction.Startup))]
namespace PedreizorFunction
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddPedreizor();
        }
    }
}
