using Moq;
using NUnit.Framework;
using RockPaperScissors.Interfaces;
using RockPaperScissors.Models;
using RockPaperScissors.StateManagers;
using System.Threading.Tasks;

namespace RockPaperScissors.Tests.Unit.StateManagers
{
    public class DeclareMidResultStateManagerTests
    {
        [TestCase(1, 2)]
        [TestCase(2, 1)]
        public async Task EnterState_MidGame_Winner(int leadPlayerNumber, int losingPlayerNumber)
        {
            // Arrange
            var result = new GameResult();
            var gameState = new GameState();
            var outputDevice = new Mock<IOutputDevice>();
            var analyser = new Mock<IGameStateAnalyser>();
            var winningPlayer = new Player { Number = leadPlayerNumber };
            var losingPlayer = new Player { Number = losingPlayerNumber };
            analyser.Setup(i => i.GetWinningPlayer()).ReturnsAsync(winningPlayer);
            analyser.Setup(i => i.GetLosingPlayer()).ReturnsAsync(losingPlayer);
            analyser.Setup(i => i.GameOver()).ReturnsAsync(false);

            var sut = new DeclareMidResultStateManager(
                gameState,
                outputDevice.Object,
                analyser.Object);

            // Act
            var newState = await sut.EnterState();

            // Assert
            outputDevice.Verify(i => i.DeclareMidResultWinner(winningPlayer, losingPlayer));
            outputDevice.Verify(i => i.NextRound());
            Assert.AreEqual(GameFlowState.ChooseHand, newState);
        }

        [TestCase(0)]
        [TestCase(1)]
        public async Task EnterState_MidGame_Draw(int rounds)
        {
            // Arrange
            var result = new GameResult();
            var gameState = new GameState { Players = new[] { new Player { RoundsWon = rounds } } };
            var outputDevice = new Mock<IOutputDevice>();
            var analyser = new Mock<IGameStateAnalyser>();
            analyser.Setup(i => i.GetWinningPlayer()).ReturnsAsync((Player)null);
            analyser.Setup(i => i.GameOver()).ReturnsAsync(false);

            var sut = new DeclareMidResultStateManager(
                gameState,
                outputDevice.Object,
                analyser.Object);

            // Act
            var newState = await sut.EnterState();

            // Assert
            outputDevice.Verify(i => i.DeclareMidResultDraw(rounds));
            outputDevice.Verify(i => i.DeclareMidResultWinner(It.IsAny<Player>(), It.IsAny<Player>()), Times.Never);
            outputDevice.Verify(i => i.NextRound());
            Assert.AreEqual(GameFlowState.ChooseHand, newState);
        }

        [TestCase(1, 2)]
        [TestCase(2, 1)]
        public async Task EnterState_GameOver(int leadPlayerNumber, int losingPlayerNumber)
        {
            // Arrange
            var result = new GameResult();
            var gameState = new GameState();
            var outputDevice = new Mock<IOutputDevice>();
            var analyser = new Mock<IGameStateAnalyser>();
            var winningPlayer = new Player { Number = leadPlayerNumber };
            var losingPlayer = new Player { Number = losingPlayerNumber };
            analyser.Setup(i => i.GetWinningPlayer()).ReturnsAsync(winningPlayer);
            analyser.Setup(i => i.GetLosingPlayer()).ReturnsAsync(losingPlayer);
            analyser.Setup(i => i.GameOver()).ReturnsAsync(true);

            var sut = new DeclareMidResultStateManager(
                gameState,
                outputDevice.Object,
                analyser.Object);

            // Act
            var newState = await sut.EnterState();

            // Assert
            outputDevice.Verify(i => i.DeclareMidResultWinner(winningPlayer, losingPlayer));
            outputDevice.Verify(i => i.NextRound(), Times.Never);
            Assert.AreEqual(GameFlowState.DeclareFinalResult, newState);
        }
    }
}
