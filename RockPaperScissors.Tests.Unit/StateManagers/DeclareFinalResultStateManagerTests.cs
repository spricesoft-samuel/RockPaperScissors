using Moq;
using NUnit.Framework;
using RockPaperScissors.Interfaces;
using RockPaperScissors.Models;
using RockPaperScissors.StateManagers;
using System.Threading.Tasks;

namespace RockPaperScissors.Tests.Unit.StateManagers
{
    public class DeclareFinalResultStateManagerTests
    {
        [TestCase(true, GameFlowState.ChooseNumberOfPlayers)]
        [TestCase(false, GameFlowState.Stopping)]
        public async Task EnterState_FullTest(bool newGame, GameFlowState expectedNewState)
        {
            // Arrange
            var result = new GameResult();
            var inputDevice = new Mock<IInputDevice>();
            inputDevice.Setup(i => i.CheckForNewGame()).ReturnsAsync(newGame);
            var outputDevice = new Mock<IOutputDevice>();
            var gameAnalyser = new Mock<IGameStateAnalyser>();
            var winningPlayer = new Player();
            var losingPlayer = new Player();
            gameAnalyser.Setup(i => i.GetWinningPlayer()).ReturnsAsync(winningPlayer);
            gameAnalyser.Setup(i => i.GetLosingPlayer()).ReturnsAsync(losingPlayer);

            var sut = new DeclareFinalResultStateManager(
                inputDevice.Object,
                outputDevice.Object,
                gameAnalyser.Object);

            // Act
            var newState = await sut.EnterState();

            // Assert
            outputDevice.Verify(i => i.DeclareFinalResult(winningPlayer, losingPlayer));
            outputDevice.Verify(i => i.PromptForNewGame());
            inputDevice.Verify(i => i.CheckForNewGame());
            Assert.AreEqual(expectedNewState, newState);
        }
    }
}
