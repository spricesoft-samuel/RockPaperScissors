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
            inputDevice.Setup(i => i.GetPlayerName(It.IsAny<Player>())).ReturnsAsync(sampleName);
            var outputDevice = new Mock<IOutputDevice>();
            var sut = new EnterPlayerNamesStateManager(inputDevice.Object, outputDevice.Object, gameState);

            // Act
            var result = await sut.EnterState();

            // Assert
            inputDevice.Verify(i => i.GetPlayerName(gameState.Players[0]));
            outputDevice.Verify(i => i.AdvisePlayer2IsCpu());
            Assert.AreEqual(2, gameState.Players.Length);
            Assert.AreEqual(sampleName, gameState.Players[0].Name);
            Assert.AreEqual(GameFlowState.ChooseHand, result);
        }
        [Test]
        public async Task EnterState_TwoPlayers()
        {
            // Arrange
            var sampleNames = new[] { "sample name1", "sample name 2" };
            var playerNameCount = -1;
            var gameState = new GameState { NumberOfPlayers = 2 };
            var inputDevice = new Mock<IInputDevice>();
            inputDevice.Setup(i => i.GetPlayerName(It.IsAny<Player>()))
                .Callback(() => playerNameCount++)
                .ReturnsAsync(() => sampleNames[playerNameCount]);
            var outputDevice = new Mock<IOutputDevice>();
            var sut = new EnterPlayerNamesStateManager(inputDevice.Object, outputDevice.Object, gameState);

            // Act
            var result = await sut.EnterState();

            // Assert
            inputDevice.Verify(i => i.GetPlayerName(gameState.Players[0]));
            inputDevice.Verify(i => i.GetPlayerName(gameState.Players[1]));
            Assert.AreEqual(2, gameState.Players.Length);
            Assert.AreEqual(sampleNames[0], gameState.Players[0].Name);
            Assert.AreEqual(sampleNames[1], gameState.Players[1].Name);
            Assert.AreEqual(GameFlowState.ChooseHand, result);
        }
    }
}
