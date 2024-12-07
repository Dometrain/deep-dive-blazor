using System.Threading;

namespace BlazorState;

public class ServerSessionManager(ISessionIdManager SessionIdManager) : ISessionManager
{ 
    private Dictionary<string, Session> _sessions = new Dictionary<string, Session>();

    public async Task<Session> GetSession()
    {
        var key = await SessionIdManager.GetSessionId();
        if (!_sessions.ContainsKey(key))
        {
            _sessions.Add(key, new Session() { SessionId = key });
        }
        var session = _sessions[key];
        
        // ensure session isn't checked out by wasm
        var endTime = DateTime.Now + TimeSpan.FromSeconds(15);
        while (session.IsCheckedOut)
        {
            if (DateTime.Now > endTime)
            {
                //If after 15 seconds the state has not yes been updated, we assume it will never do that.
                session.IsCheckedOut = false;
                //This is the old implementation: throw new TimeoutException();
            }
            else
            {
                await Task.Delay(5);
            }
        }

        return session;
    }

    public async Task UpdateSession(Session session)
    {
        if (session != null)
        {
            var key = await SessionIdManager.GetSessionId();
            session.SessionId = key;
            Replace(session, _sessions[key]);
            _sessions[key].IsCheckedOut = false;
        }
    }

    private void Replace(Session newSession, Session oldSession)
    {
        oldSession.Clear();
        foreach (var key in newSession.Keys)
            oldSession.Add(key, newSession[key]);
    }
}
