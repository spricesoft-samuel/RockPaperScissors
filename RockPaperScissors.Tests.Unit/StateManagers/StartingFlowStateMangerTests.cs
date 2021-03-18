using Moq;
using NUnit.Framework;
using RockPaperScissors.Interfaces;
using RockPaperScissors.StateManagers;
using System.Threading.Tasks;

namespace RockPaperScissors.Tests.Unit.StateManagers
{
    public class StartingFlowStateMangerTests
    {
        [Test]
        public async Task StartingStateManager_OutputsWelcomeBanner_AndSetsNewStateToWaitingForConfiguration()
        {
            // Arrange
            var outputDevice = new Mock<IOutputDevice>();
            var sut = new StartingFlowStateManager(outputDevice.Object);

            // Act
            var result = await sut.EnterState();

            // Assert
            Assert.AreEqual(GameFlowState.WaitingForConfiguration, result);
            outputDevice.Verify(i => i.WriteText(GameResources.WelcomeBanner));
        }
    }
}
