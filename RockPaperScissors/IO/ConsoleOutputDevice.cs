using RockPaperScissors.Interfaces;
using RockPaperScissors.Models;
using System;
using System.Threading.Tasks;

namespace RockPaperScissors.IO
{
    // we could test this class if we added a shim for console.
    public class ConsoleOutputDevice : IOutputDevice
    {
        public Task AdvisePlayer2IsCpu()
        {
            Console.WriteLine(GameResources.Player2isCPU);
            return Task.CompletedTask;
        }

        public Task DeclareResult(GameResult gameResult)
        {
            Console.WriteLine(GameResources.DeclareResults,
                gameResult.Players[0].Name,
                gameResult.Players[0].HandType,
                gameResult.Players[1].Name,
                gameResult.Players[1].HandType,
                gameResult.ResultReason,
                gameResult.WinningPlayerName);
            return Task.CompletedTask;
        }

        public Task DisplayInputError(string errorTemplate, params object[] errorArgs)
        {
            Console.WriteLine(errorTemplate, errorArgs);
            return Task.CompletedTask;
        }

        public Task PromptForNewGame()
        {
            Console.WriteLine(GameResources.PlayAgainPrompt);
            return Task.CompletedTask;
        }

        public Task PromptPlayerToChooseHand(Player player)
        {
            Console.WriteLine(GameResources.ChooseHand, player.Name);
            return Task.CompletedTask;
        }

        public Task PromptToChooseNumberOfPlayers()
        {
            Console.WriteLine(GameResources.ChoosePlayerNumbers);
            return Task.CompletedTask;
        }

        public Task PromptToEnterPlayerName(int playerNumber)
        {
            Console.WriteLine(GameResources.EnterNames, playerNumber);
            return Task.CompletedTask;
        }

        public Task SayGoodBye()
        {
            Console.WriteLine(GameResources.EndBanner);
            return Task.CompletedTask;
        }

        public Task WelcomeThePlayers()
        {
            Console.WriteLine(GameResources.WelcomeBanner);
            return Task.CompletedTask;
        }
    }
}
