using RockPaperScissors.Interfaces;
using System.Threading.Tasks;

namespace RockPaperScissors.StateManagers
{
    public class ChoosePlayerNumberStateManager : IFlowStateManager
    {
        private readonly IOutputDevice _outputDevice;
        private readonly IInputDevice _inputDevice;

        public ChoosePlayerNumberStateManager(
            IInputDevice inputDevice,
            IOutputDevice outputDevice)
        {
            _inputDevice = inputDevice;
            _outputDevice = outputDevice;
        }

        public GameFlowState ManagedState => GameFlowState.ChooseNumberOfPlayers;

        public async Task<GameFlowState> EnterState()
        {
            await _outputDevice.WriteText(GameResources.ChoosePlayerNumbers);

            var playerNumber = await _inputDevice.GetUserInput(1, 2);
            
            return GameFlowState.Stopping;
        }
    }
}
