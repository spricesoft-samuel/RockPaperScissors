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
            var mockStateManager = new Mock<IFlowStateManager>();
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

        [Test]
        public async Task GameStateManger_ChangeFlowState_LoopsOnlyUntilFlowState_IsStopped()
        {
            // Arrange
            var state1 = new Mock<IFlowStateManager>();
            state1.Setup(i => i.EnterState()).ReturnsAsync(GameFlowState.WaitingForConfiguration);
            var state2 = new Mock<IFlowStateManager>();
            state2.Setup(i => i.EnterState()).ReturnsAsync(GameFlowState.Stopping);
            var state3 = new Mock<IFlowStateManager>();
            state3.Setup(i => i.EnterState()).ReturnsAsync(GameFlowState.Stopped);

            var stateRepository = new Mock<IStateChangeManagerRepository>();
            stateRepository.Setup(i => i.GetStateManager(GameFlowState.Starting))
                .ReturnsAsync(state1.Object);
            stateRepository.Setup(i => i.GetStateManager(GameFlowState.WaitingForConfiguration))
                .ReturnsAsync(state2.Object);
            stateRepository.Setup(i => i.GetStateManager(GameFlowState.Stopping))
                .ReturnsAsync(state3.Object);

            var sut = new GameStateManager(stateRepository.Object);

            // Act
            await sut.ChangeFlowState(GameFlowState.Starting);
            var finalState = await sut.GetFlowState();

            // Assert
            Assert.AreEqual(GameFlowState.Stopped, finalState);
            stateRepository.Verify(i => i.GetStateManager(GameFlowState.Starting), Times.Once);
            state1.Verify(i => i.EnterState(), Times.Once);
            state2.Verify(i => i.EnterState(), Times.Once);
            state3.Verify(i => i.EnterState(), Times.Once);
        }


        [TestCase(GameFlowState.Starting)]
        [TestCase(GameFlowState.WaitingForConfiguration)]
        [TestCase(GameFlowState.Stopping)]
        public async Task GameStateManger_GetFlowState_ReturnsPreviouslySetState(GameFlowState testState)
        {
            // Arrange
            GameFlowState setState = GameFlowState.Unset;
            var stateRepository = new Mock<IStateChangeManagerRepository>();
            var mockStateManager = new Mock<IFlowStateManager>();
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
