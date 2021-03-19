﻿using RockPaperScissors.Interfaces;
using RockPaperScissors.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RockPaperScissors.IO
{
    // Todo: dont block ctrl-c when taking input
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

        public async Task<HandType> GetHandInput()
        {
            var key = await GetKeyInput(true, 'R', 'P', 'S', 'r', 'p', 's');
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
    }
}
