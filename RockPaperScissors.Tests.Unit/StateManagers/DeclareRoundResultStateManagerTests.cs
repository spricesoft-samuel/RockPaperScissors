using Moq;
using NUnit.Framework;
using RockPaperScissors.Interfaces;
using RockPaperScissors.Models;
using RockPaperScissors.StateManagers;
using System.Threading.Tasks;

namespace RockPaperScissors.Tests.Unit.StateManagers
{
    public class DeclareRoundResultStateManagerTests
    {
        [Test]
        public async Task EnterState_FullTest()
        {
            // Arrange
            var result = new GameResult();
            var gameState = new GameState();
            var outputDevice = new Mock<IOutputDevice>();
            var resultProcessor = new Mock<IResultProcessor>();
            resultProcessor
                .Setup(i => i.ProcessGameStateResult(It.IsAny<GameState>()))
                .ReturnsAsync(result);
            var sut = new DeclareRoundResultStateManager(
                outputDevice.Object,
                gameState,
                resultProcessor.Object);

            // Act
            var newState = await sut.EnterState();

            // Assert
            resultProcessor.Verify(i => i.ProcessGameStateResult(gameState));
            outputDevice.Verify(i => i.DeclareRoundResult(result));
            Assert.AreEqual(GameFlowState.DeclareMidResult, newState);
        }
    }
}
