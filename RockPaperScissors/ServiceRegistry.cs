using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RockPaperScissors.Interfaces;
using RockPaperScissors.IO;
using RockPaperScissors.Models;
using RockPaperScissors.StateManagers;
using System;
using System.IO;

namespace RockPaperScissors
{
    public class ServiceRegistry
    {
        public static void Register(IServiceCollection services)
        {
            services.AddSingleton<GameState>();
            services.AddHostedService<GameHost>();
            services.AddSingleton<IOutputDevice, ConsoleOutputDevice>();
            services.AddSingleton<IInputDevice, ConsoleInputDevice>();
            services.AddSingleton<IStateChangeManagerRepository, StateChangeManagerRepository>();
            services.AddSingleton<IGameStateManager, GameStateManager>();
            services.AddSingleton<IResultProcessor, ResultProcessor>();
            services.AddSingleton<IGameStateAnalyser, GameStateAnalyser>();
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
