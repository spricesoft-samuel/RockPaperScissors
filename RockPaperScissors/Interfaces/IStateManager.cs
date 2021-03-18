using System.Threading.Tasks;

namespace RockPaperScissors.Interfaces
{
    public interface IStateManager
    {
        GameState ManagedState { get; }
        Task<GameState> EnterState();
    }
}