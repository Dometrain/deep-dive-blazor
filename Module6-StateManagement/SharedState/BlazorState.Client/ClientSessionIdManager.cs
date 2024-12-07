
using Microsoft.JSInterop;

namespace BlazorState;

public class ClientSessionIdManager(IJSRuntime JsRuntime) : ISessionIdManager
{
    public async Task<string?> GetSessionId()
    {
        var result = await JsRuntime.InvokeAsync<string>("ReadCookie.ReadCookie", "sessionId");
        return result;
    }
}
