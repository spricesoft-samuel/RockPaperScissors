using RockPaperScissors.Models;
using System.Threading.Tasks;

namespace RockPaperScissors.Interfaces
{
    public interface IFlowStateManager
    {
        GameFlowState ManagedState { get; }
        Task<GameFlowState> EnterState();
    }
}