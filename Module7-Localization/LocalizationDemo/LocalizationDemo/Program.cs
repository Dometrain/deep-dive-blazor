using LocalizationDemo.Client.Pages;
using LocalizationDemo.Components;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddLocalization();
builder.Services.AddControllers();

var app = builder.Build();
string[] supportedCultures = ["en-US", "sv-SE"];
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);
localizationOptions.RequestCultureProviders.Clear();
localizationOptions.RequestCultureProviders = new List<IRequestCultureProvider>
{
    new CookieRequestCultureProvider(),
    new CustomAcceptLanguageHeaderRequestCultureProvider()
};

app.UseRequestLocalization(localizationOptions);

app.MapGet("/Culture/Set", (string culture, string redirectUri, HttpContext httpContext) =>
{
    if (!string.IsNullOrWhiteSpace(culture))
    {
        httpContext.Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(
                new RequestCulture(culture, culture)));
    }

    return Results.LocalRedirect(redirectUri);
});


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(LocalizationDemo.Client._Imports).Assembly);

app.Run();


public class CustomAcceptLanguageHeaderRequestCultureProvider : RequestCultureProvider
{
    public int MaximumAcceptLanguageHeaderValuesToTry { get; set; } = 3;

    public override Task<ProviderCultureResult?> DetermineProviderCultureResult(HttpContext httpContext)
    {
        ArgumentNullException.ThrowIfNull(httpContext);

        var acceptLanguageHeader = httpContext.Request.GetTypedHeaders().AcceptLanguage;

        if (acceptLanguageHeader == null || acceptLanguageHeader.Count == 0)
        {
            return NullProviderCultureResult;
        }

        var languages = acceptLanguageHeader.AsEnumerable();


        if (MaximumAcceptLanguageHeaderValuesToTry > 0)
        {
            // We take only the first configured number of languages from the header and then order those that we
            // attempt to parse as a CultureInfo to mitigate potentially spinning CPU on lots of parse attempts.
            languages = languages.Take(MaximumAcceptLanguageHeaderValuesToTry);
        }

        var orderedLanguages = languages
           .OrderByDescending(h => h, StringWithQualityHeaderValueComparer.QualityComparer)
           .Select(x => NormalizeCulture(x.Value))
           .ToList();


        if (orderedLanguages.Count > 0)
        {
            return Task.FromResult<ProviderCultureResult?>(new ProviderCultureResult(orderedLanguages));
        }

        return NullProviderCultureResult;
    }

    private StringSegment NormalizeCulture(StringSegment cultureSegment)
    {
        if (cultureSegment == null || cultureSegment.Value == null)
        {
            return new StringSegment(CultureInfo.CurrentCulture.Name); // Fallback to current culture
        }

        var culture = cultureSegment.Value;

        // Map generic language tags to specific cultures
        var cultureMap = new Dictionary<string, string>
        {
            { "sv", "sv-SE" },
            { "en", "en-US" },
            { "fr", "fr-FR" }
            // Add more mappings as needed
        };

        if (cultureMap.TryGetValue(culture, out var mappedCulture))
        {
            return new StringSegment(mappedCulture);
        }

        // If no mapping, return the original StringSegment
        return cultureSegment;
    }

}