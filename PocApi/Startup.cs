using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nudes.Pedreizor;
using Nudes.Pedreizor.Configuration;
using Nudes.RazorRenderer;

namespace PocApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; private set; }
        public IWebHostEnvironment Environment { get; }

        public Startup(IWebHostEnvironment environment)
        {
            var builder = new ConfigurationBuilder()
                                    .SetBasePath(environment.ContentRootPath)
                                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                                    .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true)
                                    .AddEnvironmentVariables();

            Configuration = builder.Build();
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddRazorPages().AddRazorRenderer();

            services.AddPedreizor(() => new PedreizorOptions
            {
                PageCounterVisible = true,
                PageCounterPosition = PageNumberPosition.FooterRight,
                Paper = PaperType.A4
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseEndpoints(options => options.MapControllers());
        }
    }
}
