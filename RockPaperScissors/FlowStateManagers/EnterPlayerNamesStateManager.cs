using RockPaperScissors.Interfaces;
using RockPaperScissors.Models;
using System.Linq;
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
            _gameState.Players = new[] { new Player { Number = 1 }, new Player { Number = 2 } };

            for (var i = 0; i < _gameState.NumberOfPlayers; i++)
            {
                _gameState.Players[i].Name = await _inputDevice.GetPlayerName(_gameState.Players[i]);
            }

            if (_gameState.NumberOfPlayers == 1)
            {
                await _outputDevice.AdvisePlayer2IsCpu();
                _gameState.Players[1].IsCpu = true;
                _gameState.Players[1].Name = "CPU";
            }

            return GameFlowState.ChooseHand;
        }
    }
}
