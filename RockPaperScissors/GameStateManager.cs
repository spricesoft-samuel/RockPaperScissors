using RockPaperScissors.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    public class GameStateManager : IGameStateManager
    {
        private readonly IStateChangeManagerRepository _stateRepository;
        private GameState _gameState;

        public GameStateManager(
            IStateChangeManagerRepository stateRepository)
        {
            _gameState = GameState.Unset;
            _stateRepository = stateRepository;
        }

        public async Task ChangeState(GameState state)
        {
            _gameState = state;
            while(_gameState != GameState.Stopping)
            {
                var stateManager = await _stateRepository.GetStateManager(state);
                _gameState = await stateManager.EnterState();
            }
        }

        public Task<GameState> GetState()
        {
            return Task.FromResult(_gameState);
        }
    }
}
