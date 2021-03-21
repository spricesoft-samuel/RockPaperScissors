using RockPaperScissors.Interfaces;
using RockPaperScissors.Models;
using System.Linq;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    public class GameStateAnalyser : IGameStateAnalyser
    {
        public const int ROUNDS_TO_WIN = 2;
        private readonly GameState _gameState;

        public GameStateAnalyser(GameState gameState)
        {
            _gameState = gameState;
        }

        public Task<Player> GetWinningPlayer()
        {
            var leadPlayer = _gameState.Players[0].RoundsWon > _gameState.Players[1].RoundsWon
                ? _gameState.Players[0]
                : _gameState.Players[1].RoundsWon > _gameState.Players[0].RoundsWon
                ? _gameState.Players[1] : null;

            return Task.FromResult(leadPlayer);
        }

        public async Task<Player> GetLosingPlayer()
        {
            var winningplayer = await GetWinningPlayer();
            return winningplayer == null
                ? null
                : _gameState.Players.First(p => p.Number != winningplayer.Number);
        }

        public Task<bool> GameOver()
        {
            return Task.FromResult(_gameState.Players.Any(p => p.RoundsWon == ROUNDS_TO_WIN));
        }
    }
}
