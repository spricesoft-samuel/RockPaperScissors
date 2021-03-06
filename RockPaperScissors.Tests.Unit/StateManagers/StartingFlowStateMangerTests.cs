using Moq;
using NUnit.Framework;
using RockPaperScissors.Interfaces;
using RockPaperScissors.Models;
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
            Assert.AreEqual(GameFlowState.ChooseNumberOfPlayers, result);
            outputDevice.Verify(i => i.WelcomeThePlayers());
        }
    }
}
