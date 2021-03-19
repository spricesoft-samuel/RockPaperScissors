using Moq;
using NUnit.Framework;
using RockPaperScissors.Interfaces;
using RockPaperScissors.Models;
using RockPaperScissors.StateManagers;
using System.Linq;
using System.Threading.Tasks;

namespace RockPaperScissors.Tests.Unit.StateManagers
{
    public class ChooseHandStateManagerTests
    {
        private static readonly HandType[] ValidInGameHandTypes = new[]
        {
            HandType.Rock,
            HandType.Paper,
            HandType.Scissors
        };

        [TestCase(HandType.Rock)]
        [TestCase(HandType.Paper)]
        [TestCase(HandType.Scissors)]
        public async Task EnterState_SinglePlayer(HandType expected)
        {
            // Arrange
            var gameState = new GameState 
            {
                Players = new[] { new Player(), new Player { IsCpu = true } },
                NumberOfPlayers = 1,
            };
            var inputDevice = new Mock<IInputDevice>();
            inputDevice.Setup(i => i.GetHandInput(gameState.Players[0])).ReturnsAsync(expected);
            var outputDevice = new Mock<IOutputDevice>();
            var sut = new ChooseHandStateManager(inputDevice.Object, outputDevice.Object, gameState);

            // Act
            var result = await sut.EnterState();

            // Assert
            outputDevice.Verify(i => i.PromptPlayerToChooseHand(gameState.Players[0]), Times.Once);
            inputDevice.Verify(i => i.GetHandInput(gameState.Players[0]), Times.Once);
            inputDevice.Verify(i => i.GetHandInput(gameState.Players[1]), Times.Never);
            Assert.AreEqual(expected, gameState.Players[0].HandType);
            CollectionAssert.IsSubsetOf(new[] { gameState.Players[1].HandType }, ValidInGameHandTypes);
            Assert.AreEqual(GameFlowState.DeclareResult, result);
        }

        [TestCase(HandType.Rock, HandType.Rock)]
        [TestCase(HandType.Paper, HandType.Paper)]
        [TestCase(HandType.Scissors, HandType.Scissors)]

        [TestCase(HandType.Rock, HandType.Paper)]
        [TestCase(HandType.Paper, HandType.Scissors)]
        [TestCase(HandType.Scissors, HandType.Rock)]

        [TestCase(HandType.Rock, HandType.Scissors)]
        [TestCase(HandType.Paper, HandType.Rock)]
        [TestCase(HandType.Scissors, HandType.Paper)]
        public async Task EnterState_DualPlayer(HandType expectedP1, HandType expectedP2)
        {
            // Arrange
            var gameState = new GameState
            {
                Players = new[] { new Player(), new Player() },
                NumberOfPlayers = 2,
            };
            var inputDevice = new Mock<IInputDevice>();
            inputDevice.Setup(i => i.GetHandInput(gameState.Players[0])).ReturnsAsync(expectedP1);
            inputDevice.Setup(i => i.GetHandInput(gameState.Players[1])).ReturnsAsync(expectedP2);
            var outputDevice = new Mock<IOutputDevice>();
            var sut = new ChooseHandStateManager(inputDevice.Object, outputDevice.Object, gameState);

            // Act
            var result = await sut.EnterState();

            // Assert
            outputDevice.Verify(i => i.PromptPlayerToChooseHand(gameState.Players[0]), Times.Once);
            outputDevice.Verify(i => i.PromptPlayerToChooseHand(gameState.Players[1]), Times.Once);
            inputDevice.Verify(i => i.GetHandInput(gameState.Players[0]), Times.Once);
            inputDevice.Verify(i => i.GetHandInput(gameState.Players[1]), Times.Once);
            Assert.AreEqual(expectedP1, gameState.Players[0].HandType);
            Assert.AreEqual(expectedP2, gameState.Players[1].HandType);
            Assert.AreEqual(GameFlowState.DeclareResult, result);
        }


        [Test]
        public async Task EnterState_Cpu_RandomTest()
        {

            // Act
            // try 500 times
            var results = await Task.WhenAll(
                Enumerable.Range(1, 500)
                .Select(i => RunRandTest())
                .ToList()
            );

            // Assert
            CollectionAssert.Contains(results, HandType.Rock);
            CollectionAssert.Contains(results, HandType.Paper);
            CollectionAssert.Contains(results, HandType.Scissors);
        }

        private async Task<HandType> RunRandTest()
        {

            // Arrange
            var gameState = new GameState
            {
                Players = new[] { new Player(), new Player { IsCpu = true } },
                NumberOfPlayers = 1,
            };
            var inputDevice = new Mock<IInputDevice>();
            inputDevice.Setup(i => i.GetHandInput(gameState.Players[0])).ReturnsAsync(HandType.Rock);
            var outputDevice = new Mock<IOutputDevice>();
            var sut = new ChooseHandStateManager(inputDevice.Object, outputDevice.Object, gameState);

            await sut.EnterState();

            return gameState.Players[1].HandType;
        }
    }
}
