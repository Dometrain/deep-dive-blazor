using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorState;

public static class ServiceCollectionExtensions
{
    public static void AddBlazorClientState(this IServiceCollection services)
    {
        services.AddSingleton<ISessionManager,ClientSessionManager>();
        services.AddTransient<ISessionIdManager,ClientSessionIdManager>();
    }
}
