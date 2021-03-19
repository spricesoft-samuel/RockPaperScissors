using RockPaperScissors.Interfaces;
using RockPaperScissors.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RockPaperScissors.IO
{
    public class ConsoleInputDevice : IInputDevice
    {
        private readonly IOutputDevice _outputDevice;
        private readonly GameState _gameState;
        private bool _ctrlCpressed;

        public ConsoleInputDevice(
            IOutputDevice outputDevice,
            GameState gameState)
        {
            _outputDevice = outputDevice;
            _gameState = gameState;

            Console.CancelKeyPress += new ConsoleCancelEventHandler(CtrlC_Handler);
        }

        private void CtrlC_Handler(object sender, ConsoleCancelEventArgs e)
        {
            _ctrlCpressed = true;
        }

        public async Task<string> GetUserInput(params string[] validResponses)
        {
            bool isValidResponse;
            string responseText;
            do
            {
                responseText = Console.ReadLine();
                isValidResponse = !validResponses.Any() || validResponses.Contains(responseText);
                if (isValidResponse != true)
                {
                    await _outputDevice.WriteText("Invalid input, please enter one of the following:" +
                        string.Join(" or ", validResponses));
                }

            } while (isValidResponse == false && 
                    !_gameState.CancellationToken.IsCancellationRequested);

            return responseText;
        }
    }
}
