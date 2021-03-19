using Moq;
using NUnit.Framework;
using RockPaperScissors.Interfaces;
using RockPaperScissors.Models;
using RockPaperScissors.StateManagers;
using System.Threading.Tasks;

namespace RockPaperScissors.Tests.Unit.StateManagers
{
    public class EnterPlayerNamesStateManagerTests
    {
        [Test]
        public async Task EnterState_SinglePlayer()
        {
            // Arrange
            var sampleName = "sample name";
            var gameState = new GameState { NumberOfPlayers = 1 };
            var inputDevice = new Mock<IInputDevice>();
            inputDevice.Setup(i => i.GetUserInput()).ReturnsAsync(sampleName);
            var outputDevice = new Mock<IOutputDevice>();
            var sut = new EnterPlayerNamesStateManager(inputDevice.Object, outputDevice.Object, gameState);

            // Act
            var result = await sut.EnterState();

            // Assert
            outputDevice.Verify(i => i.WriteText(GameResources.EnterNames, new[] { "1" }));
            inputDevice.Verify(i => i.GetUserInput());
            Assert.AreEqual(1, gameState.PlayerNames.Length);
            Assert.AreEqual(sampleName, gameState.PlayerNames[0]);
            Assert.AreEqual(GameFlowState.Stopping, result);
        }
        [Test]
        public async Task EnterState_TwoPlayers()
        {
            // Arrange
            var sampleNames = new[] { "sample name1", "sample name 2" };
            var playerNameCount = -1;
            var gameState = new GameState { NumberOfPlayers = 2 };
            var inputDevice = new Mock<IInputDevice>();
            inputDevice.Setup(i => i.GetUserInput())
                .Callback(() => playerNameCount++)
                .ReturnsAsync(() => sampleNames[playerNameCount]);
            var outputDevice = new Mock<IOutputDevice>();
            var sut = new EnterPlayerNamesStateManager(inputDevice.Object, outputDevice.Object, gameState);

            // Act
            var result = await sut.EnterState();

            // Assert
            outputDevice.Verify(i => i.WriteText(GameResources.EnterNames, new[] { "1" }));
            outputDevice.Verify(i => i.WriteText(GameResources.EnterNames, new[] { "2" }));
            inputDevice.Verify(i => i.GetUserInput());
            Assert.AreEqual(2, gameState.PlayerNames.Length);
            Assert.AreEqual(sampleNames[0], gameState.PlayerNames[0]);
            Assert.AreEqual(sampleNames[1], gameState.PlayerNames[1]);
            Assert.AreEqual(GameFlowState.Stopping, result);
        }
    }
}
