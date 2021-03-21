using NUnit.Framework;
using RockPaperScissors.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors.Tests.Unit
{
    public class GameStateAnalyserTests
    {
        [TestCase(2,0,1,2)]
        [TestCase(1,2,2,1)]
        [TestCase(0,0,null,null)]
        [TestCase(1,0,1,2)]
        [TestCase(1,1,null,null)]
        [TestCase(0,1,2,1)]
        public async Task GetWinningPlayer_GetLosingPlayer(
            int p1GamesWon, 
            int p2GamesWon,
            int? expectedWinnerNumber,
            int? expectedLoserNumber)
        {
            // Arrange
            var gameState = new GameState
            {
                Players = new[] 
                {
                    new Player { RoundsWon = p1GamesWon, Number = 1 },
                    new Player { RoundsWon = p2GamesWon, Number = 2 },
                },
            };
            var sut = new GameStateAnalyser(gameState);

            // Act
            var winner = await sut.GetWinningPlayer();
            var loser = await sut.GetLosingPlayer();

            // Assert
            Assert.AreEqual(expectedWinnerNumber, winner?.Number);
            Assert.AreEqual(expectedLoserNumber, loser?.Number);
        }

        [TestCase(0, 0, false)]
        [TestCase(0, 1, false)]
        [TestCase(1, 0, false)]
        [TestCase(1, 1, false)]
        [TestCase(2, 0, true)]
        [TestCase(2, 1, true)]
        [TestCase(0, 2, true)]
        [TestCase(1, 2, true)]
        public async Task GameOver(int p1Wins, int p2Wins, bool expectGameOver)
        {

            // Arrange
            var gameState = new GameState
            {
                Players = new[]
                {
                    new Player { RoundsWon = p1Wins, Number = 1 },
                    new Player { RoundsWon = p2Wins, Number = 2 },
                },
            };
            var sut = new GameStateAnalyser(gameState);

            // Act
            var result = await sut.GameOver();

            // Assert
            Assert.AreEqual(expectGameOver, result);
        }
    }
}
