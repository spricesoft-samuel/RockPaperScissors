using Moq;
using NUnit.Framework;
using RockPaperScissors.Interfaces;
using RockPaperScissors.Models;
using RockPaperScissors.StateManagers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors.Tests.Unit.StateManagers
{
    public class DeclareResultStateManagerTests
    {
        [TestCase(true, GameFlowState.ChooseHand)]
        [TestCase(false, GameFlowState.Stopping)]
        public async Task EnterState_FullTest(bool newGame, GameFlowState expectedNewState)
        {
            // Arrange
            var result = new GameResult();
            var gameState = new GameState();
            var inputDevice = new Mock<IInputDevice>();
            inputDevice.Setup(i => i.CheckForNewGame()).ReturnsAsync(newGame);
            var outputDevice = new Mock<IOutputDevice>();
            var resultProcessor = new Mock<IResultProcessor>();
            resultProcessor
                .Setup(i => i.ProcessGameStateResult(It.IsAny<GameState>()))
                .ReturnsAsync(result);
            var sut = new DeclareResultStateManager(
                inputDevice.Object,
                outputDevice.Object,
                gameState,
                resultProcessor.Object);

            // Act
            var newState = await sut.EnterState();

            // Assert
            resultProcessor.Verify(i => i.ProcessGameStateResult(gameState));
            outputDevice.Verify(i => i.DeclareResult(result));
            outputDevice.Verify(i => i.PromptForNewGame());
            inputDevice.Verify(i => i.CheckForNewGame());
            Assert.AreEqual(expectedNewState, newState);
        }
    }
}
