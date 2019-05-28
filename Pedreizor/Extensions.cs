using Microsoft.Extensions.DependencyInjection;
using Nudes.Pedreizor.Internal;
using Nudes.Pedreizor.RazorRenderer;
using System;
using System.IO;
using System.Reflection;

namespace Nudes.Pedreizor
{
    public static class PedreizorExtensions
    {
        /// <summary>
        /// Add Razor renderer transient instance for dependency injection
        /// </summary>
        /// <param name="builder">Instance of IMvcCoreBuilder</param>
        public static IMvcCoreBuilder AddRazorRenderer(this IMvcCoreBuilder builder)
        {
            builder.Services.AddTransient<IRazorRenderer, RazorRenderer.RazorRenderer>();

            return builder;
        }

        /// <summary>
        /// Add Razor renderer transient instance for dependency injection
        /// </summary>
        /// <param name="builder">Instance of IMvcBuilder</param>
        public static IMvcBuilder AddRazorRenderer(this IMvcBuilder builder)
        {
            builder.Services.AddTransient<IRazorRenderer, RazorRenderer.RazorRenderer>();

            return builder;
        }

        /// <summary>
        /// Load unmanaged library for current operational system to use tools to create pdf files
        /// </summary>
        public static void LoadWebkitLibrary()
        {
            if (!ConverterContainer.LibrariesAlreadyLoaded)
            {
                var size = IntPtr.Size == 4 ? "x86" : "x64";
                var assembly = Assembly.GetExecutingAssembly();
                string _path = Path.Combine(assembly.Location.Replace($"{assembly.GetName().Name}.dll", ""), $"Libs\\{size}\\libwkhtmltox.{OperationalSystem.GetLibExtension()}");
                new CustomAssemblyLoadContext().LoadUnmanagedLibrary(_path);
                ConverterContainer.LibrariesAlreadyLoaded = true;
            }
        }

        /// <summary>
        /// Inject pedreizor library how transient and customized options
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="options">Custom pedreizor options</param>
        /// <returns>Updated service collection</returns>
        public static IServiceCollection AddPedreizor(this IServiceCollection services, PedreizorOptions options)
        {
            LoadWebkitLibrary();

            return services.AddTransient<IPedreizor, Pedreizor>(service => new Pedreizor(service.GetService<IRazorRenderer>(), options))
                           .AddTransient<PedreizorOptions>(_ => options);
        }

        /// <summary>
        /// Inject pedreizor library how transient and your default options
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <returns>Updated service collection</returns>
        public static IServiceCollection AddPedreizor(this IServiceCollection services)
            => services.AddPedreizor(new PedreizorOptions());

        /// <summary>
        /// Inject pedreizor library how transient and your default options
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="config">Generating function of configuration</param>
        /// <returns>Updated service collection</returns>
        public static IServiceCollection AddPedreizor(this IServiceCollection services, Func<PedreizorOptions> config) 
            => AddPedreizor(services, config());
    }
}
