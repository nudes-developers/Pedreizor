using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System;
using System.Threading.Tasks;

namespace Nudes.Pedreizor.RazorRenderer
{
    public interface IRazorRenderer
    {
        Task<string> Render(Uri viewPath);

        Task<string> Render<TModel>(Uri viewPath, TModel model) where TModel : class;

        IView FindView(ActionContext actionContext, Uri viewPath);
    }
}
