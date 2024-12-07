namespace BlazorState;

/// <summary>
/// Per-user session data. The object must be 
/// serializable via JSON.
/// </summary>
public class Session : Dictionary<string, string>
{
    public string SessionId { get; set; } = string.Empty;
    public bool IsCheckedOut { get; set; }
}
