using RockPaperScissors.Interfaces;
using RockPaperScissors.Models;
using System.Threading.Tasks;

namespace RockPaperScissors.StateManagers
{
    public class StartingFlowStateManager : IFlowStateManager
    {
        private readonly IOutputDevice _outputDevice;

        public StartingFlowStateManager(IOutputDevice outputDevice)
        {
            _outputDevice = outputDevice;
        }

        public GameFlowState ManagedState => GameFlowState.Starting;

        public async Task<GameFlowState> EnterState()
        {
            await _outputDevice.WriteText(GameResources.WelcomeBanner);
            return GameFlowState.ChooseNumberOfPlayers;
        }
    }
}

