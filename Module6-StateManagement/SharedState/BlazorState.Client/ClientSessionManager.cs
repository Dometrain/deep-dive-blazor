using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace BlazorState;

public class ClientSessionManager(NavigationManager NavigationManager) : ISessionManager
{
    private Session _session;

    public async Task<Session> GetSession()
    {
        var client = new HttpClient();
        client.BaseAddress =  new Uri(NavigationManager.BaseUri);
        var result = await client.GetFromJsonAsync<Session>("state");
        _session = result;
        return _session;
    }

    public async Task UpdateSession(Session session)
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri(NavigationManager.BaseUri);
        await client.PutAsJsonAsync<Session>("state", session);
        _session = session;
    }
}
