using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
namespace BlazorState.Server;

public static class StateEndpoints
{
    public static void MapStateEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/state", async (ISessionManager sessionManager, ILoggerFactory loggerFactory) =>
        {
            var logger = loggerFactory.CreateLogger("StateEndpoints");
            logger.LogInformation("Fetching session...");
            var session = await sessionManager.GetSession();
            session.IsCheckedOut = true;
            return Results.Ok(session);
        })
        .WithName("GetState");

        endpoints.MapPut("/state", async (ISessionManager sessionManager, Session updatedSession) =>
        {
            await sessionManager.UpdateSession(updatedSession);
            return Results.NoContent();
        })
        .WithName("UpdateState");
    }
}
