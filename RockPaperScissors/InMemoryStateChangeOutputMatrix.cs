using RockPaperScissors.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    public class InMemoryStateChangeOutputMatrix : IStateChangeOutputMatrix
    {
        // an in-memory db of game state output texts
        private static readonly Dictionary<GameState, string> _gameStateChangeOutputMatrix = new Dictionary<GameState, string>
        {
            { GameState.Starting, GameResources.WelcomeBanner },
            { GameState.Stopped, GameResources.EndBanner },
        };

        public Task<string> GetOutputForNewState(GameState state)
        {
            return Task.FromResult(
                _gameStateChangeOutputMatrix[state]
            );
        }
    }
}
