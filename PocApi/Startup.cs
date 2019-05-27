using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Nudes.Pedreizor;
using Nudes.Pedreizor.Configuration;
using Nudes.Pedreizor.RazorRenderer;
using PocApi.Template;

namespace PocApi
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddRazorRenderer();

            PedreizorExtensions.LoadWebkitLibrary();

            services.AddTransient<IPedreizor, Pedreizor>(service => new Pedreizor(service.GetService<IRazorRenderer>())
            {
                Title = "Doc title",
                Paper = PapersType.A3,
                PageCounterPosition = PageNumberPosition.Left,
                PageCounterVisible = true
            });
            services.AddTransient<ContractTemplate>();
            services.AddTransient<ContractTemplate2>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });
        }
    }
}
