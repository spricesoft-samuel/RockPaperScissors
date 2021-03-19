﻿using Moq;
using NUnit.Framework;
using RockPaperScissors.Interfaces;
using RockPaperScissors.Models;
using RockPaperScissors.StateManagers;
using System.Threading.Tasks;

namespace RockPaperScissors.Tests.Unit.StateManagers
{
    public class ChoosePlayerNumberStateManagerTests
    {
        [TestCase("1", 1)]
        [TestCase("2", 2)]
        public async Task EnterState_FullTest(string input, int expected)
        {
            // Arrange
            var gameState = new GameState();
            var inputDevice = new Mock<IInputDevice>();
            inputDevice.Setup(i => i.GetUserInput("1", "2")).ReturnsAsync(input);
            var outputDevice = new Mock<IOutputDevice>();
            var sut = new ChoosePlayerNumberStateManager(inputDevice.Object, outputDevice.Object, gameState);

            // Act
            var result = await sut.EnterState();

            // Assert
            outputDevice.Verify(i => i.WriteText(GameResources.ChoosePlayerNumbers));
            inputDevice.Verify(i => i.GetUserInput("1", "2"));
            Assert.AreEqual(expected, gameState.NumberOfPlayers);
            Assert.AreEqual(GameFlowState.EnterPlayerNames, result);
        }
    }
}
