using RockPaperScissors.Interfaces;
using RockPaperScissors.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    public class ResultProcessor : IResultProcessor
    {
        public const string PAPER_BEATS_ROCK = "Paper beats rock";
        public const string SCISSORS_BEATS_PAPER = "Scissors beats paper";
        public const string ROCK_BEATS_SCISSORS = "Rock beats scissors";

        public const string NO_WINNER = "Neither";
        public const string ITS_A_DRAW = "It was a draw";

        private static readonly Dictionary<StateCombo, ResultCombo> ResultMatrix =
            new Dictionary<StateCombo, ResultCombo>
            {
                {
                    new StateCombo { Hand1 = HandType.Paper, Hand2 = HandType.Rock },
                    new ResultCombo { Winner = 0, Reason = PAPER_BEATS_ROCK }
                },
                {
                    new StateCombo { Hand1 = HandType.Rock, Hand2 = HandType.Paper },
                    new ResultCombo { Winner = 1, Reason = PAPER_BEATS_ROCK }
                },
                {
                    new StateCombo { Hand1 = HandType.Scissors, Hand2 = HandType.Paper },
                    new ResultCombo { Winner = 0, Reason = SCISSORS_BEATS_PAPER }
                },
                {
                    new StateCombo { Hand1 = HandType.Paper, Hand2 = HandType.Scissors },
                    new ResultCombo { Winner = 1, Reason = SCISSORS_BEATS_PAPER }
                },
                {
                    new StateCombo { Hand1 = HandType.Rock, Hand2 = HandType.Scissors },
                    new ResultCombo { Winner = 0, Reason = ROCK_BEATS_SCISSORS }
                },
                {
                    new StateCombo { Hand1 = HandType.Scissors, Hand2 = HandType.Rock },
                    new ResultCombo { Winner = 1, Reason = ROCK_BEATS_SCISSORS }
                },
            };

        public Task<GameResult> ProcessGameStateResult(GameState gameState)
        {
            var result = new GameResult { Players = gameState.Players };
            if (gameState.Players[0].HandType == gameState.Players[1].HandType)
            {
                result.WinningPlayerName = NO_WINNER;
                result.ResultReason = ITS_A_DRAW;
            }
            else
            {
                var stateCombo = new StateCombo 
                {
                    Hand1 = gameState.Players[0].HandType, 
                    Hand2 = gameState.Players[1].HandType
                };
                var resultCombo = ResultMatrix[stateCombo];
                result.ResultReason = resultCombo.Reason;
                result.WinningPlayerName = gameState.Players[resultCombo.Winner].Name;
            }
            return Task.FromResult(result);
        }

        private struct StateCombo
        {
            public HandType Hand1;
            public HandType Hand2;
        }

        private struct ResultCombo
        {
            public int Winner;
            public string Reason;
        }
    }
}
