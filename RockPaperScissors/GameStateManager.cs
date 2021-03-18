using RockPaperScissors.Interfaces;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    public class GameStateManager : IGameStateManager
    {
        private readonly IOutputDevice _outputDevice;
        private readonly IStateChangeOutputMatrix _stateChangeOutputMatrix;
        private GameState _gameState;

        public GameStateManager(
            IOutputDevice outputDevice,
            IStateChangeOutputMatrix stateChangeOutputMatrix)
        {
            _gameState = GameState.Unset;
            _outputDevice = outputDevice;
            _stateChangeOutputMatrix = stateChangeOutputMatrix;
        }

        public async Task ChangeState(GameState state)
        {
            _gameState = state;
            var stateChangeText = await _stateChangeOutputMatrix.GetOutputForNewState(state);
            await _outputDevice.WriteText(stateChangeText);
        }

        public Task<GameState> GetState()
        {
            return Task.FromResult(_gameState);
        }
    }
}
