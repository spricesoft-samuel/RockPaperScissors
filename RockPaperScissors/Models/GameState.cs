using System.Threading;

namespace RockPaperScissors.Models
{
    public class GameState
    {
        public GameFlowState FlowState { get; set; }
        public CancellationToken CancellationToken { get; set; }
        public int NumberOfPlayers { get; set; }
        public Player[] Players { get; set; }
        public int PlayersTurn { get; set; }
        public string WinningPlayerName { get; set; }
        public string ResultReason { get; set; }
    }
}
