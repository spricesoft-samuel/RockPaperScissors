using Microsoft.Extensions.Hosting;
using RockPaperScissors.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    public class GameHost : BackgroundService
    {
        private readonly IGameStateManager _gameStateManager;

        public GameHost(IGameStateManager gameStateManager)
        {
            _gameStateManager = gameStateManager;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _gameStateManager.Start(stoppingToken);
            return Task.CompletedTask;
        }
    }
}
