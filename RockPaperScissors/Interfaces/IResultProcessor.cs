using RockPaperScissors.Models;
using System.Threading.Tasks;

namespace RockPaperScissors.Interfaces
{
    public interface IResultProcessor
    {
        Task<GameResult> ProcessGameStateResult(GameState gameState);
    }
}
