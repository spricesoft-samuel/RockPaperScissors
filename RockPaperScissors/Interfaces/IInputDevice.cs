using System.Collections.Generic;
using System.Threading.Tasks;

namespace RockPaperScissors.Interfaces
{
    public interface IInputDevice
    {
        Task<string> GetUserInput(params string[] validResponses);
    }
}
