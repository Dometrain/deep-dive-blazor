using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;

namespace JavaScriptDemo.Services;

[SupportedOSPlatform("browser")]
public partial class InteropService
{
    [JSImport("getGreeting", "InteropServiceModule")]
    public static partial string GetGreeting(string name);  

    [JSImport("globalThis.alert")] 
    public static partial void Alert(string message);        

    [JSImport("getTimezone","InteropServiceModule")]
    public static partial string GetTimezone();


    [JSImport("setupShortcuts", "InteropServiceModule")]
    public static partial void SetupShortcuts();
    
    [JSExport]
    public static void TriggerShortcut(string key)
    {
        InteropService.Alert("Save triggered");
    }

}
