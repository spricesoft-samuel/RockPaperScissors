using RockPaperScissors.Interfaces;
using RockPaperScissors.Models;
using System.Threading;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    public class GameStateManager : IGameStateManager
    {
        private readonly IStateChangeManagerRepository _stateRepository;
        private readonly GameState _gameState;

        public GameStateManager(
            IStateChangeManagerRepository stateRepository,
            GameState gameState
            )
        {
            _gameState = gameState;
            _stateRepository = stateRepository;
        }

        public void Start(CancellationToken cancellationToken)
        {
            _gameState.CancellationToken = cancellationToken;
            var game = ChangeFlowState(GameFlowState.Starting);
        }

        public async Task ChangeFlowState(GameFlowState flowState)
        {
            _gameState.FlowState = flowState;
            while(_gameState.FlowState != GameFlowState.Stopped && !_gameState.CancellationToken.IsCancellationRequested)
            {
                var stateManager = await _stateRepository.GetStateManager(_gameState.FlowState);
                _gameState.FlowState = await stateManager.EnterState();
            }
        }

        public Task<GameFlowState> GetFlowState()
        {
            return Task.FromResult(_gameState.FlowState);
        }
    }
}
