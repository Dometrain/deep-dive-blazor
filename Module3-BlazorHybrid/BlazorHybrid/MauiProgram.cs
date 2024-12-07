using BlazorHybrid.Components;
using BlazorHybrid.Components.Pages;
using BlazorHybrid.Services;
using Microsoft.Extensions.Logging;

namespace BlazorHybrid
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("Font Awesome 6 Free-Solid-900.otf", "FASolid");
                });

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddSingleton<CounterService>();
            builder.Services.AddScoped <NewPage1>();
            builder.Services.AddScoped<NativeService>();
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif 

            return builder.Build();
        }
    }
}
