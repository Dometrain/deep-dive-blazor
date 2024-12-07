using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using System.Globalization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddLocalization();

var host = builder.Build();

const string defaultCulture = "en-US";

var js = host.Services.GetRequiredService<IJSRuntime>();
var result = await js.InvokeAsync<string>("cookieManager.getCultureCookie");
var cultureString = defaultCulture;
if (!string.IsNullOrEmpty(result))
{
    // Example: c=sv-SE|uic=sv-SE
    var parts = result.Split('|');
    var cPart = parts.FirstOrDefault(p => p.StartsWith("c="));
    cultureString = cPart?.Split('=')[1]; // Extract the value after "c="
}
var culture = CultureInfo.GetCultureInfo(cultureString ?? defaultCulture);

CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;

await host.RunAsync();
