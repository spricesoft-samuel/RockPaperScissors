using RockPaperScissors.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    public class StateChangeManagerRepository : IStateChangeManagerRepository
    {
        private readonly IEnumerable<IStateManager> _states;

        public StateChangeManagerRepository(IEnumerable<IStateManager> states)
        {
            _states = states;
        }

        public Task<IStateManager> GetStateManager(GameState state)
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
