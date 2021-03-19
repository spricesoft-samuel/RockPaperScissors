using System.Collections.Generic;
using System.Threading.Tasks;

namespace RockPaperScissors.Interfaces
{
    public interface IInputDevice
    {
        Task<int> GetUserInput(params int[] validResponses);
    }
}
