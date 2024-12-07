using Microsoft.AspNetCore.Components.WebView.Maui;

namespace BlazorHybrid.Components;

public class BlazorShellContent:ShellContent
{
	public BlazorShellContent()
	{
        this.ParentChanging += BlazorShellContent_ParentChanging;
	}

    private void BlazorShellContent_ParentChanging(object? sender, ParentChangingEventArgs e)
    {
        if (!loaded)
        {
            Content = new BlazorContentPage()
            {
                
                StartPath = StartPath,
                Parameters=Parameters,
                Selector=Selector,
                ComponentType=ComponentType,

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