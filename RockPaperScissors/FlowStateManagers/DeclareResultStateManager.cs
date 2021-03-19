using RockPaperScissors.Interfaces;
using RockPaperScissors.Models;
using System;
using System.Threading.Tasks;

namespace RockPaperScissors.StateManagers
{
    public class DeclareResultStateManager : IFlowStateManager
    {
        private readonly IOutputDevice _outputDevice;
        private readonly GameState _gameState;
        private readonly IResultProcessor _resultProcessor;
        private readonly IInputDevice _inputDevice;

        public DeclareResultStateManager(
            IInputDevice inputDevice,
            IOutputDevice outputDevice,
            GameState gameState,
            IResultProcessor resultProcessor)
        {
            _inputDevice = inputDevice;
            _outputDevice = outputDevice;
            _gameState = gameState;
            _resultProcessor = resultProcessor;
        }

        public GameFlowState ManagedState => GameFlowState.DeclareResult;

        public async Task<GameFlowState> EnterState()
        {
            var result = await _resultProcessor.ProcessGameStateResult(_gameState);
            await _outputDevice.DeclareResult(result);
            await _outputDevice.PromptForNewGame();
            var newGame = await _inputDevice.CheckForNewGame();
            if (newGame)
            {
                return GameFlowState.ChooseHand;
            }

            return GameFlowState.Stopping;
        }
    }
}
