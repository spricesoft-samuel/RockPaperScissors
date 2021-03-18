﻿using RockPaperScissors.Interfaces;
using System.Threading.Tasks;

namespace RockPaperScissors.StateManagers
{
    public class StoppingFlowStateManager : IStateManager
    {
        private readonly IOutputDevice _outputDevice;

        public GameFlowState ManagedState => GameFlowState.Stopping;

        public StoppingFlowStateManager(IOutputDevice outputDevice)
        {
            _outputDevice = outputDevice;
        }

        public Task<GameFlowState> EnterState()
        {
            _outputDevice.WriteText(GameResources.EndBanner);
            return Task.FromResult(GameFlowState.Stopped);
        }
    }
}
