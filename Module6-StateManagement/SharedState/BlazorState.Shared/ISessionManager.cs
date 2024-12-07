namespace BlazorState;

public interface ISessionManager
{
    Task<Session> GetSession();
    Task UpdateSession(Session session);
}
