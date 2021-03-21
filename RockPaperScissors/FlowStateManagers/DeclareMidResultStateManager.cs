using RockPaperScissors.Interfaces;
using RockPaperScissors.Models;
using System.Linq;
using System.Threading.Tasks;

namespace RockPaperScissors.StateManagers
{
    public class DeclareMidResultStateManager : IFlowStateManager
    {
        private readonly GameState _gameState;
        private readonly IOutputDevice _outputDevice;
        private readonly IGameStateAnalyser _gameStateAnalyser;

        public DeclareMidResultStateManager(
            GameState gameState,
            IOutputDevice outputDevice,
            IGameStateAnalyser gameStateAnalyser)
        {
            _gameState = gameState;
            _outputDevice = outputDevice;
            _gameStateAnalyser = gameStateAnalyser;
        }

        public GameFlowState ManagedState => GameFlowState.DeclareMidResult;

        public async Task<GameFlowState> EnterState()
        {
            var leadPlayer = await _gameStateAnalyser.GetWinningPlayer();

            if (leadPlayer == null)
            {
                await _outputDevice.DeclareMidResultDraw(_gameState.Players.First().RoundsWon);
            }
            else
            {
                await _outputDevice.DeclareMidResultWinner(
                    leadPlayer,
                    await _gameStateAnalyser.GetLosingPlayer());
            }
            
            if (await _gameStateAnalyser.GameOver())
            {
                return GameFlowState.DeclareFinalResult;
            }

            await _outputDevice.NextRound();
            return GameFlowState.ChooseHand;
        }
    }
}
