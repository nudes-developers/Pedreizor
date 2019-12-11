using Microsoft.Extensions.DependencyInjection;

namespace Nudes.RazorRenderer
{
    public static class Extensions
    {
        /// <summary>
        /// Add Razor renderer transient instance for dependency injection
        /// </summary>
        /// <param name="builder">Instance of IMvcCoreBuilder</param>
        public static IMvcCoreBuilder AddRazorRenderer(this IMvcCoreBuilder builder)
        {
            builder.Services.AddTransient<IRazorRenderer, IRazorRenderer>();

            return builder;
        }

        /// <summary>
        /// Add Razor renderer transient instance for dependency injection
        /// </summary>
        /// <param name="builder">Instance of IMvcBuilder</param>
        public static IMvcBuilder AddRazorRenderer(this IMvcBuilder builder)
        {
            builder.Services.AddTransient<IRazorRenderer, IRazorRenderer>();

            return builder;
        }
    }
}
