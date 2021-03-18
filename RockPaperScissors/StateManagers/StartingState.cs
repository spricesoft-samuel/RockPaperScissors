using RockPaperScissors.Interfaces;
using System.Threading.Tasks;

namespace RockPaperScissors.StateManagers
{
    public class StartingState : IStateManager
    {
        private readonly IOutputDevice _outputDevice;

        public StartingState(IOutputDevice outputDevice)
        {
            _outputDevice = outputDevice;
        }

        public GameState ManagedState => GameState.Starting;

        public async Task<GameState> EnterState()
        {
            await _outputDevice.WriteText(GameResources.WelcomeBanner);
            return GameState.Stopping;
        }
    }
}
