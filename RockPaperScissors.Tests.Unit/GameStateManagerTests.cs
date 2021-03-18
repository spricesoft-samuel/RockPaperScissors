using Moq;
using NUnit.Framework;
using RockPaperScissors.Interfaces;
using System.Threading.Tasks;

namespace RockPaperScissors.Tests.Unit
{
    public class GameStateManagerTests
    {
        [Test]
        public async Task GameStateManger_ChangeFlowState_StateStopped_DoesNothing()
        {
            // Arrange
            var stateRepository = new Mock<IStateChangeManagerRepository>();
            var sut = new GameStateManager(stateRepository.Object);

            // Act
            await sut.ChangeFlowState(GameFlowState.Stopped);

            // Assert
            stateRepository.Verify(i => i.GetStateManager(It.IsAny<GameFlowState>()), Times.Never);
        }

        [Test]
        public async Task GameStateManger_ChangeFlowState_StateStarted_GetsNewStateFromCurrent()
        {
            // Arrange
            var mockStateManager = new Mock<IStateManager>();
            mockStateManager.Setup(i => i.EnterState()).ReturnsAsync(GameFlowState.Stopped);
            var stateRepository = new Mock<IStateChangeManagerRepository>();
            stateRepository.Setup(i => i.GetStateManager(GameFlowState.Starting))
                .ReturnsAsync(mockStateManager.Object);
            var sut = new GameStateManager(stateRepository.Object);

            // Act
            await sut.ChangeFlowState(GameFlowState.Starting);
            var finalState = await sut.GetFlowState();

            // Assert
            Assert.AreEqual(GameFlowState.Stopped, finalState);
            stateRepository.Verify(i => i.GetStateManager(GameFlowState.Starting), Times.Once);
            mockStateManager.Verify(i => i.EnterState(), Times.Once);
        }


        [TestCase(GameFlowState.Starting)]
        [TestCase(GameFlowState.WaitingForConfiguration)]
        [TestCase(GameFlowState.Stopping)]
        public async Task GameStateManger_GetFlowState_ReturnsPreviouslySetState(GameFlowState testState)
        {
            // Arrange
            GameFlowState setState = GameFlowState.Unset;
            var stateRepository = new Mock<IStateChangeManagerRepository>();
            var mockStateManager = new Mock<IStateManager>();
            stateRepository.Setup(i => i.GetStateManager(It.IsAny<GameFlowState>())).ReturnsAsync(mockStateManager.Object);
            var sut = new GameStateManager(stateRepository.Object);
            mockStateManager.Setup(i => i.EnterState())
                .Callback(async () => 
                {
                    setState = await sut.GetFlowState();
                })
                .ReturnsAsync(GameFlowState.Stopped);


            // Act
            await sut.ChangeFlowState(testState);
            await sut.GetFlowState();

            // Assert
            Assert.AreEqual(testState, setState);
        }
    }
}
