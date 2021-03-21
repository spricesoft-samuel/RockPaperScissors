using NUnit.Framework;
using RockPaperScissors.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors.Tests.Unit
{
    public class ResultProcessorTests
    {
        [TestCase(HandType.Rock, HandType.Rock, ResultProcessor.ITS_A_DRAW, null)]
        [TestCase(HandType.Paper, HandType.Paper, ResultProcessor.ITS_A_DRAW, null)]
        [TestCase(HandType.Scissors, HandType.Scissors, ResultProcessor.ITS_A_DRAW, null)]

        [TestCase(HandType.Rock, HandType.Paper, ResultProcessor.PAPER_BEATS_ROCK, 2)]
        [TestCase(HandType.Paper, HandType.Rock, ResultProcessor.PAPER_BEATS_ROCK, 1)]
        [TestCase(HandType.Scissors, HandType.Paper, ResultProcessor.SCISSORS_BEATS_PAPER, 1)]
        [TestCase(HandType.Paper, HandType.Scissors, ResultProcessor.SCISSORS_BEATS_PAPER, 2)]
        [TestCase(HandType.Scissors, HandType.Rock, ResultProcessor.ROCK_BEATS_SCISSORS, 2)]
        [TestCase(HandType.Rock, HandType.Scissors, ResultProcessor.ROCK_BEATS_SCISSORS, 1)]
        public async Task ProcessGameStateResult_ExpectedResults(
            HandType p1Hand, 
            HandType p2hand,
            string expectedResultReason,
            int? expectedWinner)
        {
            // Arrange
            var gameState = new GameState
            {
                Players = new[] 
                {
                    new Player { HandType = p1Hand, Name = "p1", Number = 1 },
                    new Player { HandType = p2hand, Name = "p2", Number = 2 }
                },
            };
            var sut = new ResultProcessor();

            // Act
            var result = await sut.ProcessGameStateResult(gameState);

            // Assert
            Assert.AreEqual(expectedResultReason, result.ResultReason);
            Assert.AreEqual(expectedWinner, result.WinningPlayer?.Number);
        }
    }
}
