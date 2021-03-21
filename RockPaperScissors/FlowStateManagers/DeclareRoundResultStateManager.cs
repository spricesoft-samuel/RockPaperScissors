using RockPaperScissors.Interfaces;
using RockPaperScissors.Models;
using System.Threading.Tasks;

namespace RockPaperScissors.StateManagers
{
    public class DeclareRoundResultStateManager : IFlowStateManager
    {
        private readonly IOutputDevice _outputDevice;
        private readonly GameState _gameState;
        private readonly IResultProcessor _resultProcessor;

        public DeclareRoundResultStateManager(
            IOutputDevice outputDevice,
            GameState gameState,
            IResultProcessor resultProcessor)
        {
            _outputDevice = outputDevice;
            _gameState = gameState;
            _resultProcessor = resultProcessor;
        }

        public GameFlowState ManagedState => GameFlowState.DeclareRoundResult;

        public async Task<GameFlowState> EnterState()
        {
            var result = await _resultProcessor.ProcessGameStateResult(_gameState);
            await _outputDevice.DeclareRoundResult(result);

            return GameFlowState.DeclareMidResult;
        }
    }
}
