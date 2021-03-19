using System.Collections.Generic;
using System.Threading.Tasks;

namespace RockPaperScissors.Interfaces
{
    public interface IStateChangeManagerRepository
    {
        Task<IFlowStateManager> GetStateManager(GameFlowState state);
    }
}