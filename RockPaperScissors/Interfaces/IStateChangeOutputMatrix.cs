using System.Threading.Tasks;

namespace RockPaperScissors.Interfaces
{
    public interface IStateChangeOutputMatrix
    {
        Task<string> GetOutputForNewState(GameState state);
    }
}