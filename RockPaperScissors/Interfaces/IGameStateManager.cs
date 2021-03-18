using System.Threading.Tasks;

namespace RockPaperScissors.Interfaces
{
    public interface IGameStateManager
    {
        Task ChangeState(GameState state);
        Task<GameState> GetState();
    }
}