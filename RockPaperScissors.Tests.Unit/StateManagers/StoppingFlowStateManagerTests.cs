using Moq;
using NUnit.Framework;
using RockPaperScissors.Interfaces;
using RockPaperScissors.Models;
using RockPaperScissors.StateManagers;
using System.Threading.Tasks;

namespace RockPaperScissors.Tests.Unit.StateManagers
{
    public class StoppingFlowStateManagerTests
    {
        [Test]
        public async Task StoppingFlowStateManager_OutputsWelcomeBanner_AndSetsNewFlowStateToStopped()
        {
            // Arrange
            var outputDevice = new Mock<IOutputDevice>();
            var sut = new StoppingFlowStateManager(outputDevice.Object);

            // Act
            var result = await sut.EnterState();

            // Assert
            Assert.AreEqual(GameFlowState.Stopped, result);
            outputDevice.Verify(i => i.SayGoodBye());
        }
    }
}
