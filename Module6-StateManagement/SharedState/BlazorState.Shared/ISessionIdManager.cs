namespace BlazorState;

public interface ISessionIdManager
{
    Task<string?> GetSessionId();
}
