﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Nudes.Pedreizor.RazorRenderer
{
    public class RazorRenderer : IRazorRenderer
    {
        private readonly IRazorViewEngine viewEngine;
        private readonly ITempDataProvider tempDataProvider;
        private readonly IServiceProvider serviceProvider;

        public RazorRenderer(IRazorViewEngine viewEngine, ITempDataProvider tempDataProvider, IServiceProvider serviceProvider)
        {
            this.viewEngine = viewEngine;
            this.tempDataProvider = tempDataProvider;
            this.serviceProvider = serviceProvider;
        }

        public async Task<string> Render<TModel>(Uri viewPath, TModel model) where TModel : class
        {
            var actionContext = GetEmptyActionContext();
            var view = FindView(actionContext, viewPath);

            using (var writer = new StringWriter())
            {
                var viewContext = new ViewContext(actionContext: actionContext,
                    view: view,
                    viewData: new ViewDataDictionary<TModel>(metadataProvider: new EmptyModelMetadataProvider(), modelState: new ModelStateDictionary())
                    {
                        Model = model
                    },
                tempData: new TempDataDictionary(actionContext.HttpContext, tempDataProvider),
                writer: writer,
                htmlHelperOptions: new HtmlHelperOptions());

                await view.RenderAsync(viewContext);

                return writer.ToString();
            }
        }

        public IView FindView(ActionContext actionContext, Uri viewPath)
        {
            var getViewResult = viewEngine.GetView(null, viewPath.ToString(), true);

            if (getViewResult.Success) return getViewResult.View;

            var findViewResult = viewEngine.FindView(actionContext, viewPath.ToString(), isMainPage: true);

            if (findViewResult.Success) return findViewResult.View;

            throw new InvalidOperationException($"Unable to find view '{viewPath}'. The following locations were searched: {string.Join("\n", findViewResult.SearchedLocations)}");
        }

        private ActionContext GetEmptyActionContext()
        {
            var httpContext = new DefaultHttpContext
            {
                RequestServices = serviceProvider
            };
            return new ActionContext(httpContext, new RouteData(), new ActionDescriptor());
        }

        public async Task<string> Render(Uri viewPath)
        {
            var actionContext = GetEmptyActionContext();
            var view = FindView(actionContext, viewPath);

            using (var writer = new StringWriter())
            {
                var viewContext = new ViewContext(actionContext: actionContext,
                    view: view,
                    viewData: new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary()),
                    tempData: new TempDataDictionary(actionContext.HttpContext, tempDataProvider),
                    writer: writer,
                    htmlHelperOptions: new HtmlHelperOptions());

                await view.RenderAsync(viewContext);

                return writer.ToString();
            }
        }
    }
}
