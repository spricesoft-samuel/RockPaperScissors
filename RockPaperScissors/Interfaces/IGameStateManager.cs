using System.Threading;
using System.Threading.Tasks;

namespace RockPaperScissors.Interfaces
{
    public interface IGameStateManager
    {
        Task ChangeFlowState(GameFlowState state);
        Task<GameFlowState> GetFlowState();
        void Start(CancellationToken cancellationToken);
    }
}