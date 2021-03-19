using RockPaperScissors.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    public class StateChangeManagerRepository : IStateChangeManagerRepository
    {
        private readonly IEnumerable<IFlowStateManager> _states;

        public StateChangeManagerRepository(IEnumerable<IFlowStateManager> states)
        {
            _states = states;
        }

        public Task<IFlowStateManager> GetStateManager(GameFlowState state)
        {
            var stateManager = _states.FirstOrDefault(i => i.ManagedState == state);
            if (stateManager == null)
            {
                throw new InvalidProgramException("There should be exactly one manager for each state");
            }

            return Task.FromResult(stateManager);
        }
    }
}
