using RockPaperScissors.Interfaces;
using System;
using System.Threading.Tasks;

namespace RockPaperScissors.IO
{
    public class ConsoleOutputDevice : IOutputDevice
    {
        // This implementation is difficult to unit test and is better tested through an integration or system test.
        public Task WriteText(string text)
        {
            Console.WriteLine(text);
            return Task.CompletedTask;
        }

        public Task WriteText(string template, params string[] arguments)
        {
            Console.WriteLine(template, arguments);
            return Task.CompletedTask;
        }
    }
}
