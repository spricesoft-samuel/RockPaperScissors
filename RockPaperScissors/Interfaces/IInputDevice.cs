using RockPaperScissors.Models;
using System.Threading.Tasks;

namespace RockPaperScissors.Interfaces
{
    public interface IInputDevice
    {
        Task<int> GetNumberOfPlayers();
        Task<HandType> GetHandInput(Player player);
        Task<string> GetPlayerName(Player player);
    }
}
