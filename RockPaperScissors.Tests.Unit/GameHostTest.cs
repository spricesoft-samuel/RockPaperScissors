using Moq;
using NUnit.Framework;
using RockPaperScissors.Interfaces;
using System.Threading.Tasks;

namespace RockPaperScissors.Tests.Unit
{
    public class GameHostTest
    {
        //[Test]
        //public async Task StartAsync_ChangesState_to_started()
        //{
        //    // Arrange
        //    var stateManager = new Mock<IGameStateManager>();
        //    var sut = new GameHost(stateManager.Object);

        //    // Act
        //    await sut.StartAsync();

        //    // Assert
        //    stateManager.Verify(i => i.ChangeFlowState(GameFlowState.Starting), Times.Once);
        //}

        //[Test]
        //public async Task StopAsync_ChangesState_to_stopping()
        //{
        //    // Arrange
        //    var stateManager = new Mock<IGameStateManager>();
        //    var sut = new GameHost(stateManager.Object);

        //    // Act
        //    await sut.StopAsync();

        //    // Assert
        //    stateManager.Verify(i => i.ChangeFlowState(GameFlowState.Stopping), Times.Once);
        //}
    }
}