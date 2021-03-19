using RockPaperScissors.Interfaces;
using RockPaperScissors.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    public class ConsoleInputDevice : IInputDevice
    {
        private readonly IOutputDevice _outputDevice;
        private readonly GameState _gameState;

        public ConsoleInputDevice(
            IOutputDevice outputDevice,
            GameState gameState)
        {
            _outputDevice = outputDevice;
            _gameState = gameState;
        }


        public async Task<int> GetUserInput(params int[] validResponses)
        {
            int userResponse;
            bool isValidResponse;
            do
            {
                // Todo: Console.Readline/readkey seems to swallow ctrl-c until an input is received.
                var responseKey = Console.ReadKey(true).KeyChar;
                userResponse = (int)char.GetNumericValue(responseKey);
                isValidResponse = validResponses.Contains(userResponse);
                if (isValidResponse != true)
                {
                    await _outputDevice.WriteText("Invalid input, please enter one of the following:" +
                        string.Join(" or ", validResponses));
                }

            } while (isValidResponse == false && 
                    !_gameState.CancellationToken.IsCancellationRequested);

            return userResponse;
        }
    }
}
