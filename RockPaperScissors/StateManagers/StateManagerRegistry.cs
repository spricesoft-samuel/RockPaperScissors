using Microsoft.Extensions.DependencyInjection;
using RockPaperScissors.Interfaces;

namespace RockPaperScissors.StateManagers
{
    public static class StateManagerRegistry
    {
        public static void RegisterStateManagers(this IServiceCollection services)
        {
            services.AddTransient<IStateManager, StartingState>();
        }
    }
}
