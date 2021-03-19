using RockPaperScissors.Models;
using System.Threading.Tasks;

namespace RockPaperScissors.Interfaces
{
    public interface IOutputDevice
    {
        Task WelcomeThePlayers();
        Task SayGoodBye();
        Task PromptPlayerToChooseHand(Player player);
        Task PromptToEnterPlayerName(int playerNumber);
        Task PromptToChooseNumberOfPlayers();
        Task AdvisePlayer2IsCpu();
        Task DisplayInputError(string errorTemplate, params object[] errorArgs);

        Task DeclareResult(GameResult gameResult);
        Task PromptForNewGame();
    }
}
