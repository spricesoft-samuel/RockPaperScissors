﻿using RockPaperScissors.Interfaces;
using RockPaperScissors.Models;
using System.Threading.Tasks;

namespace RockPaperScissors.StateManagers
{
    public class ChoosePlayerNumberStateManager : IFlowStateManager
    {
        private readonly IOutputDevice _outputDevice;
        private readonly GameState _gameState;
        private readonly IInputDevice _inputDevice;

        public ChoosePlayerNumberStateManager(
            IInputDevice inputDevice,
            IOutputDevice outputDevice,
            GameState gameState)
        {
            _inputDevice = inputDevice;
            _outputDevice = outputDevice;
            _gameState = gameState;
        }

        public GameFlowState ManagedState => GameFlowState.ChooseNumberOfPlayers;

        public async Task<GameFlowState> EnterState()
        {
            await _outputDevice.WriteText(GameResources.ChoosePlayerNumbers);

            var input = await _inputDevice.GetUserInput("1", "2");
            var asInt = int.Parse(input);
            _gameState.NumberOfPlayers = asInt;


            return GameFlowState.EnterPlayerNames;
        }
    }
}