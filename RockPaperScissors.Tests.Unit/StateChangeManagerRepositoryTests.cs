using Moq;
using NUnit.Framework;
using RockPaperScissors.Interfaces;
using System.Threading.Tasks;

namespace RockPaperScissors.Tests.Unit
{
    public class StateChangeManagerRepositoryTests
    {
        [Test]
        public async Task GetOutputForNewState_Returns_From_Resource_Started()
        {
            // Arrange
            var mockStartingStateManager = new Mock<IFlowStateManager>();
            mockStartingStateManager.SetupGet(i => i.ManagedState).Returns(GameFlowState.Starting);
            var mockStoppingStateManager = new Mock<IFlowStateManager>();
            mockStoppingStateManager.SetupGet(i => i.ManagedState).Returns(GameFlowState.Stopping);
            var sut = new StateChangeManagerRepository(new[] 
            {
                mockStartingStateManager.Object,
                mockStoppingStateManager.Object
            });

            // Act
            var result = await sut.GetStateManager(GameFlowState.Starting);

            // Assert
            Assert.AreEqual(result, mockStartingStateManager.Object);
        }
    }
}
