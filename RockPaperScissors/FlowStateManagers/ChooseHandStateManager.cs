using RockPaperScissors.Interfaces;
using RockPaperScissors.Models;
using System;
using System.Threading.Tasks;

namespace RockPaperScissors.StateManagers
{
    public class ChooseHandStateManager : IFlowStateManager
    {
        private readonly IOutputDevice _outputDevice;
        private readonly GameState _gameState;
        private readonly IInputDevice _inputDevice;

        public ChooseHandStateManager(
            IInputDevice inputDevice,
            IOutputDevice outputDevice,
            GameState gameState)
        {
            _inputDevice = inputDevice;
            _outputDevice = outputDevice;
            _gameState = gameState;
        }

        public GameFlowState ManagedState => GameFlowState.ChooseHand;

        public async Task<GameFlowState> EnterState()
        {
            _gameState.PlayersTurn = 0;
            while (_gameState.PlayersTurn < 2)
            {
                var player = _gameState.Players[_gameState.PlayersTurn];
                if (player.IsCpu)
                {
                    player.HandType = ChooseCpuHand();
                }
                else
                {
                    await _outputDevice.PromptPlayerToChooseHand(player);
                    player.HandType = await _inputDevice.GetHandInput(player);
                }
                _gameState.PlayersTurn += 1;
            }

            return GameFlowState.DeclareRoundResult;
        }

        private HandType ChooseCpuHand()
        {
            var rand = new Random();
            var randomInt = rand.Next(1, 4);
            return (HandType)randomInt;
        }
    }
}
