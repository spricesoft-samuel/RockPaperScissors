using RockPaperScissors.Interfaces;
using RockPaperScissors.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RockPaperScissors.IO
{
    // Todo: dont block ctrl-c when taking input
    // We could write tests for this class if we used a shim for Console
    public class ConsoleInputDevice : IInputDevice
    {
        private readonly IOutputDevice _outputDevice;
        private readonly GameState _gameState;
        private static readonly Dictionary<char, HandType> CharHandMap = new Dictionary<char, HandType>
        {
            { 'R', HandType.Rock },
            { 'P', HandType.Paper },
            { 'S', HandType.Scissors },
            { 'r', HandType.Rock },
            { 'p', HandType.Paper },
            { 's', HandType.Scissors },
        };
        private static readonly Dictionary<char, bool> YesNoBoolMap = new Dictionary<char, bool>
        {
            { 'Y', true },
            { 'y', true },
            { 'N', false },
            { 'n', false },
        };

        public ConsoleInputDevice(
            IOutputDevice outputDevice,
            GameState gameState)
        {
            _outputDevice = outputDevice;
            _gameState = gameState;
        }

        public async Task<int> GetNumberOfPlayers()
        {
            var key = await GetKeyInput(false, '1', '2');
            return (int)char.GetNumericValue(key);
        }

        public async Task<HandType> GetHandInput(Player player)
        {
            var key = await GetKeyInput(true, CharHandMap.Keys.ToArray());
            return CharHandMap[key];
        }

        public async Task<string> GetPlayerName(Player player)
        {
            await _outputDevice.PromptToEnterPlayerName(player.Number);

            return Console.ReadLine();
        }

        private async Task<char> GetKeyInput(bool hide, params char[] validInputs)
        {
            bool isValidResponse;
            char responseChar;
            do
            {
                responseChar = Console.ReadKey(hide).KeyChar;
                Console.WriteLine();
                isValidResponse = !validInputs.Any() || validInputs.Contains(responseChar);
                if (isValidResponse != true)
                {
                    await _outputDevice.DisplayInputError(
                        GameResources.InvalidInputText,
                        string.Join(" or ", validInputs));
                }

            } while (isValidResponse == false &&
                    !_gameState.CancellationToken.IsCancellationRequested);

            return responseChar;
        }

        public async Task<bool> CheckForNewGame()
        {
            var key = await GetKeyInput(false, YesNoBoolMap.Keys.ToArray());
            return YesNoBoolMap[key];
        }
    }
}
