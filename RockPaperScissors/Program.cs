using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RockPaperScissors.Interfaces;
using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
[assembly:InternalsVisibleTo("RockPaperScissors.Tests.Unit")]

namespace RockPaperScissors
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();

            await host.RunAsync();
        }

        static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args).ConfigureServices(services =>
            {
                services.AddHostedService<GameHost>();
                services.AddSingleton<IOutputDevice, ConsoleOutputDevice>();
                services.AddSingleton<IStateChangeOutputMatrix, InMemoryStateChangeOutputMatrix>();
                services.AddSingleton<IGameStateManager, GameStateManager>();

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
            });
        }
    }
}
