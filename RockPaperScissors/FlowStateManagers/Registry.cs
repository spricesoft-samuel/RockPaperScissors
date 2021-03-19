﻿using Microsoft.Extensions.DependencyInjection;
using RockPaperScissors.Interfaces;

namespace RockPaperScissors.StateManagers
{
    public static class Registry
    {
        public static void RegisterStateManagers(this IServiceCollection services)
        {
            services.AddTransient<IFlowStateManager, StartingFlowStateManager>();
            services.AddTransient<IFlowStateManager, StoppingFlowStateManager>();
            services.AddTransient<IFlowStateManager, ChoosePlayerNumberStateManager>();
            services.AddTransient<IFlowStateManager, EnterPlayerNamesStateManager>();
            services.AddTransient<IFlowStateManager, ChooseHandStateManager>();
        }
    }
}