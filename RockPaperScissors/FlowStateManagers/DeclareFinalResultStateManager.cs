using RockPaperScissors.Interfaces;
using RockPaperScissors.Models;
using System.Threading.Tasks;

namespace RockPaperScissors.StateManagers
{
    public class DeclareFinalResultStateManager : IFlowStateManager
    {
        private readonly IOutputDevice _outputDevice;
        private readonly IGameStateAnalyser _gameStateAnalyser;
        private readonly IInputDevice _inputDevice;

        public DeclareFinalResultStateManager(
            IInputDevice inputDevice,
            IOutputDevice outputDevice,
            IGameStateAnalyser gameStateAnalyser)
        {
            _inputDevice = inputDevice;
            _outputDevice = outputDevice;
            _gameStateAnalyser = gameStateAnalyser;
        }

        public GameFlowState ManagedState => GameFlowState.DeclareFinalResult;

        public async Task<GameFlowState> EnterState()
        {
            await _outputDevice.DeclareFinalResult(
                await _gameStateAnalyser.GetWinningPlayer(),
                await _gameStateAnalyser.GetLosingPlayer());
            await _outputDevice.PromptForNewGame();
            var newGame = await _inputDevice.CheckForNewGame();
            if (newGame)
            {
                return GameFlowState.ChooseNumberOfPlayers;
            }

            return GameFlowState.Stopping;
        }
    }
}
