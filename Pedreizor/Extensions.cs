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
        public static void AddRazorRenderer(this IMvcCoreBuilder builder) => builder
            .Services
            .AddTransient<IRazorRenderer, RazorRenderer.RazorRenderer>();

        /// <summary>
        /// Add Razor renderer transient instance for dependency injection
        /// </summary>
        /// <param name="builder">Instance of IMvcBuilder</param>
        public static void AddRazorRenderer(this IMvcBuilder builder) => builder
            .Services
            .AddTransient<IRazorRenderer, RazorRenderer.RazorRenderer>();

        /// <summary>
        /// Load unmanaged library for current operational system to use tools to create pdf files
        /// </summary>
        public static void LoadWebkitLibrary()
        {
            var size = IntPtr.Size == 4 ? "x86" : "x64";
            var assembly = Assembly.GetExecutingAssembly();
            string _path = Path.Combine(assembly.Location.Replace($"{assembly.GetName().Name}.dll", ""), $"Libs\\{size}\\libwkhtmltox.{OperationalSystem.GetLibExtension()}");
            new CustomAssemblyLoadContext().LoadUnmanagedLibrary(_path);
        }
    }
}
