# Pedreizor

A friendly converter of razor templates for pdf in aspnetcore environment

This library is made up of a Razor to html converter and an html to pdf converter using the [DinkToPdf](https://github.com/rdvojmoc/DinkToPdf) library as a dependency, which in turn uses the webkit pdf renderer

## Get Started

Pedreizor can be installed using the Nuget package manager or the dotnet CLI

~~~
Install-Package Pedreizor
~~~

## Razor renderer

The part responsible for transforming razor files into html strings is an instance that implements the IRazorRenderer interface and can be used by dependency injection as follows

The Startup.cs
~~~cs
services.AddMvc().AddRazorRenderer();
~~~

Any class that requires razor-html conversion
~~~cs

...

public PocController(IRazorRenderer razorRenderer)
{
    this.razorRenderer = razorRenderer;
}

...

private async Task<string> RenderingWithoutModel()
{
    string htmlString = await razorRenderer.Render(new Uri("/Template/Index.cshtml", UriKind.Relative));

    return htmlString;
}

private async Task<string> RenderingWithModel()
{
    string htmlString = await razorRenderer.Render<ExampleModel>(new Uri("/Template/Index.cshtml", UriKind.Relative), new ExampleModel
    {
        Text = "Razor converter"
    });

    return htmlString;
}
~~~

## Pedreizor

The part responsible for converting the html strings into pdf is the responsibility of Pedreizor, and can be configured and used as follows

Startup.cs
~~~cs
services.AddMvc().AddRazorRenderer();

services.AddPedreizor();
~~~

or

~~~cs
services.AddMvc().AddRazorRenderer();

services.AddPedreizor(new PedreizorOptions
{
    PageCounterVisible = true,
    PageCounterPosition = PageNumberPosition.FooterRight,
    Paper = PaperType.A4
});
~~~

or else 
~~~cs
services.AddMvc().AddRazorRenderer();

services.AddPedreizor(()=> new PedreizorOptions
{
    PageCounterVisible = true,
    PageCounterPosition = PageNumberPosition.FooterRight,
    Paper = PaperType.A4
});
~~~

Any class that requires razor-pdf or html-pdf conversion
~~~cs
...

public PocController(IRazorRenderer razorRenderer, IPedreizor pedreizor)
{
    this.pedreizor = pedreizor;
    this.razorRenderer = razorRenderer;
}

...

private async Task<Stream> ConvertingPureHtmlText()
{
    string htmlContent = "<h1>Hello World</h1>";

    Stream stream = await pedreizor.Pdfy(htmlContent);

    return stream;
}

private async Task<Stream> ConvertingPureHtmlTextOfARazorFileUsingRazorRenderer()
{
    string htmlString = await razorRenderer.Render(new Uri("/Template/Index.cshtml", UriKind.Relative));
    
    Stream stream = await pedreizor.Pdfy(htmlString);

    return stream;
}

private async Task<Stream> ConvertingPureHtmlTextOfARazorFileUsingPedreizor()
{   
    Stream stream = await pedreizor.Pdfy(new Uri("/Template/Index.cshtml", UriKind.Relative));

    return stream;
}

private async Task<Stream> ConvertingRazorWithModelUsingPedreizor()
{   
    Stream stream = await pedreizor.Pdfy<ExampleModel>(new Uri("/Template/Index.cshtml", UriKind.Relative), new ExampleModel
    {
        Text = "Pedreizor converter"
    });

    return stream;
}

private async Task ConvertingRazorWithoutModelUsingPedreizorOnExistentStream(Stream stream)
{
    await pedreizor.PdfyTo(new Uri("/Template/Index.cshtml", UriKind.Relative), stream);
}

private async Task ConvertingRazorWithModelUsingPedreizorOnExistentStream(Stream stream)
{
    await pedreizor.PdfyTo<ExampleModel>(new Uri("/Template/Index.cshtml", UriKind.Relative), stream ,new ExampleModel
    {
        Text = "Pedreizor converter"
    });
}

~~~