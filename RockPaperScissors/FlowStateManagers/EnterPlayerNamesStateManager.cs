using RockPaperScissors.Interfaces;
using RockPaperScissors.Models;
using System.Threading.Tasks;

namespace RockPaperScissors.StateManagers
{
    public class EnterPlayerNamesStateManager : IFlowStateManager
    {
        private readonly IOutputDevice _outputDevice;
        private readonly GameState _gameState;
        private readonly IInputDevice _inputDevice;

        public EnterPlayerNamesStateManager(
            IInputDevice inputDevice,
            IOutputDevice outputDevice,
            GameState gameState)
        {
            _inputDevice = inputDevice;
            _outputDevice = outputDevice;
            _gameState = gameState;
        }

        public GameFlowState ManagedState => GameFlowState.EnterPlayerNames;

        public async Task<GameFlowState> EnterState()
        {
            _gameState.PlayerNames = new string[_gameState.NumberOfPlayers];

            for (var i = 0; i < _gameState.NumberOfPlayers; i++)
            {
                await _outputDevice.WriteText(GameResources.EnterNames, (i + 1).ToString());

                _gameState.PlayerNames[i] = await _inputDevice.GetUserInput();

            }
            
            return GameFlowState.Stopping;
        }
    }
}
