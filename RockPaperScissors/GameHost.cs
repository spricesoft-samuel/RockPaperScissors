using Microsoft.Extensions.Hosting;
using RockPaperScissors.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    public class GameHost : IHostedService
    {
        private readonly IGameStateManager _gameStateManager;

        public GameHost(IGameStateManager gameStateManager)
        {
            _gameStateManager = gameStateManager;
        }

        public async Task StartAsync(CancellationToken cancellationToken = default)
        {
            await _gameStateManager.ChangeState(GameState.Starting);
        }

        public async Task StopAsync(CancellationToken cancellationToken = default)
        {
            await _gameStateManager.ChangeState(GameState.Stopping);
        }
    }
}
