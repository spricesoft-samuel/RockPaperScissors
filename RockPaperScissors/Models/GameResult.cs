namespace RockPaperScissors.Models
{
    public class GameResult
    {
        public Player[] Players { get; set; }
        public string WinningPlayerName { get; set; }
        public string ResultReason { get; set; }
    }
}
