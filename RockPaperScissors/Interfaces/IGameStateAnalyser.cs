using RockPaperScissors.Models;
using System.Threading.Tasks;

namespace RockPaperScissors.Interfaces
{
    public interface IGameStateAnalyser
    {
        Task<Player> GetWinningPlayer();
        Task<Player> GetLosingPlayer();
        Task<bool> GameOver();
    }
}