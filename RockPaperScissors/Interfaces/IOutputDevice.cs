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

        Task DeclareRoundResult(GameResult gameResult);
        Task DeclareMidResultDraw(int roundsDrawn);
        Task DeclareMidResultWinner(Player winning, Player losing);
        Task NextRound();
        Task DeclareFinalResult(Player winner, Player loser);
        Task PromptForNewGame();
    }
}
