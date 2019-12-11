using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System;
using System.Threading.Tasks;

namespace Nudes.RazorRenderer
{
    public interface IRazorRenderer
    {
        /// <summary>
        /// Create a html string from razor 
        /// </summary>
        /// <param name="viewPath">Path of razor to render</param>
        /// <returns>Html string converted</returns>
        Task<string> Render(Uri viewPath);

        /// <summary>
        /// Create a html string from razor 
        /// </summary>
        /// <typeparam name="TModel">Type of razor model</typeparam>
        /// <param name="viewPath">Path of razor to render</param>
        /// <param name="model">Instance of razor model</param>
        /// <returns>Html string converted</returns>
        Task<string> Render<TModel>(Uri viewPath, TModel model) where TModel : class;

        /// <summary>
        /// Returns a view instance in a action context from razor path
        /// </summary>
        /// <param name="actionContext">Action context of the view</param>
        /// <param name="viewPath">Razor path view</param>
        /// <returns>View instance</returns>
        IView FindView(ActionContext actionContext, Uri viewPath);
    }
}
