using RockPaperScissors.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    public class GameStateManager : IGameStateManager
    {
        private readonly IStateChangeManagerRepository _stateRepository;
        private GameFlowState _gameFlowState;

        public GameStateManager(
            IStateChangeManagerRepository stateRepository)
        {
            _gameFlowState = GameFlowState.Unset;
            _stateRepository = stateRepository;
        }

        public async Task ChangeFlowState(GameFlowState flowState)
        {
            _gameFlowState = flowState;
            while(_gameFlowState != GameFlowState.Stopped)
            {
                var stateManager = await _stateRepository.GetStateManager(_gameFlowState);
                _gameFlowState = await stateManager.EnterState();
            }
        }

        public Task<GameFlowState> GetFlowState()
        {
            return Task.FromResult(_gameFlowState);
        }
    }
}
