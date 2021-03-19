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

        public Task<IFlowStateManager> GetStateManager(GameFlowState flowState)
        {
            var stateManager = _states.Where(i => i.ManagedState == flowState).ToList();
            if (stateManager.Count() != 1)
            {
                throw new InvalidProgramException(
                    "There should be exactly one manager for each state, error flowState:" + flowState);
            }

            return Task.FromResult(stateManager.First());
        }
    }
}
