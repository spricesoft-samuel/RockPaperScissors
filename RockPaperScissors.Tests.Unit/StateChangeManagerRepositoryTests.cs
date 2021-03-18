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
            var mockStartingStateManager = new Mock<IStateManager>();
            mockStartingStateManager.SetupGet(i => i.ManagedState).Returns(GameState.Starting);
            var mockStoppingStateManager = new Mock<IStateManager>();
            mockStoppingStateManager.SetupGet(i => i.ManagedState).Returns(GameState.Stopping);
            var sut = new StateChangeManagerRepository(new[] 
            {
                mockStartingStateManager.Object,
                mockStoppingStateManager.Object
            });

            // Act
            var result = await sut.GetStateManager(GameState.Starting);

            // Assert
            Assert.AreEqual(result, mockStartingStateManager.Object);
        }
    }
}
