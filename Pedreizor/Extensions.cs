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
        public static void AddRazorRender(this IMvcCoreBuilder builder) => builder
            .Services
            .AddTransient<IRazorRenderer, RazorRenderer.RazorRenderer>();

        public static void AddRazorRender(this IMvcBuilder builder) => builder
            .Services
            .AddTransient<IRazorRenderer, RazorRenderer.RazorRenderer>();

        public static void LoadWebkitLibrary()
        {
            var size = IntPtr.Size == 4 ? "x86" : "x64";
            var assembly = Assembly.GetExecutingAssembly();
            string _path = Path.Combine(assembly.Location.Replace($"{assembly.GetName().Name}.dll", ""), $"Libs\\{size}\\libwkhtmltox.{OperationalSystem.GetLibExtension()}");
            new CustomAssemblyLoadContext().LoadUnmanagedLibrary(_path);
        }
    }
}
