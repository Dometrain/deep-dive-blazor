using Microsoft.AspNetCore.Components.WebView.Maui;

namespace BlazorHybrid.Components;

public class BlazorContentPage:ContentPage
{
	public BlazorContentPage()
	{
        this.Appearing += BlazorContentPage_Appearing;
        Shell.SetNavBarIsVisible(this, false);
	}

    private void BlazorContentPage_Appearing(object? sender, EventArgs e)
    {
        if (!loaded)
        {
            Content = new BlazorWebView()
            {
                HostPage = "wwwroot/index.html",
                StartPath = StartPath,
                BackgroundColor = Colors.Transparent,
                RootComponents =
                {
                    new RootComponent()
                    {
                        Selector=Selector,
                        ComponentType = ComponentType,
                        Parameters=Parameters
                    }
                }
            };
            loaded = true;
        }
    }

    public string StartPath { get; set; } = "";
	public string? Selector { get; set; } = "#app";
    public Type? ComponentType { get; set; }
    public IDictionary<string, object?>? Parameters { get; set; }
    private bool loaded = false;

}