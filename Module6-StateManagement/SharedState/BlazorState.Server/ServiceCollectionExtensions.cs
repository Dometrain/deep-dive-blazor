using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorState;

public static class ServiceCollectionExtensions
{
    public static void AddBlazorServerState(this IServiceCollection services)
    {
        services.AddSingleton<ISessionManager,ServerSessionManager>();
        services.AddTransient<ISessionIdManager,ServerSessionIdManager>();
    }
}
