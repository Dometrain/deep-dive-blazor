using Microsoft.AspNetCore.Http;

namespace BlazorState;

public class ServerSessionIdManager(IHttpContextAccessor HttpContextAccessor) : ISessionIdManager
{    
    public Task<string?> GetSessionId()
    {
        var httpContext = HttpContextAccessor.HttpContext;
        string? result;

        if (httpContext != null)
        {
            if (httpContext.Request.Cookies.ContainsKey("sessionId"))
            {
                result = httpContext.Request.Cookies["sessionId"];
            }
            else if (httpContext.Items.ContainsKey("sessionId"))
            {
                result = httpContext.Items["sessionId"]?.ToString();
            }
            else
            {
                result = Guid.NewGuid().ToString();
                httpContext.Response.Cookies.Append("sessionId", result);
                httpContext.Items["sessionId"] = result;
            }
        }
        else
        {
            throw new InvalidOperationException("No HttpContext available");
        }
        return Task.FromResult(result);
    }
}
