using Moq;
using NUnit.Framework;
using RockPaperScissors.Interfaces;
using System.Threading.Tasks;

namespace RockPaperScissors.Tests.Unit
{
    public class GameStateManagerTests
    {
        [Test]
        public async Task GameStateManger_ChangeState_StateStopped_DoesNothing()
        {
            // Arrange
            var stateRepository = new Mock<IStateChangeManagerRepository>();
            var sut = new GameStateManager(stateRepository.Object);

            // Act
            await sut.ChangeState(GameState.Stopping);

            // Assert
            stateRepository.Verify(i => i.GetStateManager(It.IsAny<GameState>()), Times.Never);
        }

        [Test]
        public async Task GameStateManger_ChangeState_StateStarted_GetsNewStateFromCurrent()
        {
            // Arrange
            var mockStateManager = new Mock<IStateManager>();
            mockStateManager.Setup(i => i.EnterState()).ReturnsAsync(GameState.Stopping);
            var stateRepository = new Mock<IStateChangeManagerRepository>();
            stateRepository.Setup(i => i.GetStateManager(GameState.Starting))
                .ReturnsAsync(mockStateManager.Object);
            var sut = new GameStateManager(stateRepository.Object);

            // Act
            await sut.ChangeState(GameState.Starting);
            var finalState = await sut.GetState();

            // Assert
            Assert.AreEqual(GameState.Stopping, finalState);
            stateRepository.Verify(i => i.GetStateManager(GameState.Starting), Times.Once);
            mockStateManager.Verify(i => i.EnterState(), Times.Once);
        }


        [TestCase(GameState.Starting)]
        [TestCase(GameState.WaitingForConfiguration)]
        public async Task GameStateManger_GetState_ReturnsPreviouslySetState(GameState testState)
        {
            // Arrange
            GameState setState = GameState.Unset;
            var stateRepository = new Mock<IStateChangeManagerRepository>();
            var mockStateManager = new Mock<IStateManager>();
            stateRepository.Setup(i => i.GetStateManager(It.IsAny<GameState>())).ReturnsAsync(mockStateManager.Object);
            var sut = new GameStateManager(stateRepository.Object);
            mockStateManager.Setup(i => i.EnterState())
                .Callback(async () => 
                {
                    setState = await sut.GetState();
                })
                .ReturnsAsync(GameState.Stopping);


            // Act
            await sut.ChangeState(testState);
            await sut.GetState();

            // Assert
            Assert.AreEqual(testState, setState);
        }
    }
}
