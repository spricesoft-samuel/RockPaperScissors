using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RockPaperScissors.Interfaces;
using RockPaperScissors.StateManagers;
using System;
using System.IO;

namespace RockPaperScissors
{
    public class ServiceRegistry
    {
        public static void Register(IServiceCollection services)
        {
            services.AddHostedService<GameHost>();
            services.AddSingleton<IOutputDevice, ConsoleOutputDevice>();
            services.AddSingleton<IStateChangeManagerRepository, StateChangeManagerRepository>();
            services.AddSingleton<IGameStateManager, GameStateManager>();
            services.RegisterStateManagers();

            var configbuilder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(AppContext.BaseDirectory))
                .AddJsonFile("appsettings.json", optional: true);
            var configuration = configbuilder.Build();

            services.AddLogging(builder =>
            {
                builder.AddConfiguration(configuration)
                    .AddFilter("Microsoft", LogLevel.Warning)
                    //.AddFilter("System", LogLevel.Warning)
                    //.AddFilter("NToastNotify", LogLevel.Warning)
                    .AddConsole();
            });
        }
    }
}
