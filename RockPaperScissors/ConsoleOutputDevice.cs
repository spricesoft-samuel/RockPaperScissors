using RockPaperScissors.Interfaces;
using System;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    public class ConsoleOutputDevice : IOutputDevice
    {
        // This implementation is difficult to unit test and is better tested through an integration or system test.
        public Task WriteText(string text)
        {
            Console.WriteLine(text);
            return Task.CompletedTask;
        }
    }
}
