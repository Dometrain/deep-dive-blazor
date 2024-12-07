using CustomElementsLibrary;
using CustomElementsLibrary.Pages;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.RegisterCustomElement<Counter>("my-blazor-counter");
await builder.Build().RunAsync();
