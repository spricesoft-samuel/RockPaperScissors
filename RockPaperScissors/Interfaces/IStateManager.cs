using System.Threading.Tasks;

namespace RockPaperScissors.Interfaces
{
    public interface IStateManager
    {
        GameFlowState ManagedState { get; }
        Task<GameFlowState> EnterState();
    }
}