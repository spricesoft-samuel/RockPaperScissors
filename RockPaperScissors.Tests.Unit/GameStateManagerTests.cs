using Moq;
using NUnit.Framework;
using RockPaperScissors.Interfaces;
using System.Threading.Tasks;

namespace RockPaperScissors.Tests.Unit
{
    public class GameStateManagerTests
    {
        [Test]
        public async Task GameStateManger_ChangeState_CallsOutputWithResultFromStateChangeMatrix_Started()
        {
            // Arrange
            var expectedOutput = "output string";
            var outputDevice = new Mock<IOutputDevice>();
            var stateChangeOuputMatrix = new Mock<IStateChangeOutputMatrix>();
            stateChangeOuputMatrix.Setup(i => i.GetOutputForNewState(It.IsAny<GameState>()))
                .ReturnsAsync(expectedOutput);
            var sut = new GameStateManager(outputDevice.Object, stateChangeOuputMatrix.Object);

            // Act
            await sut.ChangeState(GameState.Starting);

            // Assert
            outputDevice.Verify(i => i.WriteText(expectedOutput), Times.Once);
        }

        [TestCase(GameState.Starting)]
        [TestCase(GameState.Stopped)]
        public async Task GameStateManger_GetState_ReturnsPreviouslySetState(GameState testState)
        {
            // Arrange
            var outputDevice = new Mock<IOutputDevice>();
            var stateChangeOuputMatrix = new Mock<IStateChangeOutputMatrix>();
            var sut = new GameStateManager(outputDevice.Object, stateChangeOuputMatrix.Object);


            // Act
            await sut.ChangeState(testState);
            var result = await sut.GetState();

            // Assert
            Assert.AreEqual(testState, result);
        }
    }
}
