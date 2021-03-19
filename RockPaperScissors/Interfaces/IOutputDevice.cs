using System.Threading.Tasks;

namespace RockPaperScissors.Interfaces
{
    public interface IOutputDevice
    {
        Task WriteText(string text);
        Task WriteText(string template, params string[] arguments);
    }
}
