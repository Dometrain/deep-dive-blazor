using BlazorHybrid.Components;
using Microsoft.AspNetCore.Components.WebView.Maui;
using Microsoft.Maui.Controls.PlatformConfiguration;

namespace BlazorHybrid
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            BlazorWebViewHandler.BlazorWebViewMapper.AppendToMapping("BlazorHybridFix", (handler, view) =>
            {
#if IOS || MACCATALYST
                handler.PlatformView.Opaque = false;
                handler.PlatformView.BackgroundColor = UIKit.UIColor.Clear;
#elif WINDOWS
                handler.PlatformView.Opacity = 0;
                handler.PlatformView.DefaultBackgroundColor = new Windows.UI.Color() { A = 0, R = 0, G = 0, B = 0 };
#endif
            });
            MainPage = new MainPage();
        }
    }
}
