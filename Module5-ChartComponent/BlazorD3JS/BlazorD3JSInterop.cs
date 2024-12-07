using System.Runtime.InteropServices.JavaScript;

namespace BlazorD3JS;

public partial class BlazorD3JSInterop
{
    [JSImport("createBarChart", "BlazorD3JSModule")]
    internal static partial void CreateBarChart(string id, string data);
}