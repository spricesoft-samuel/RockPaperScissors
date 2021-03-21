namespace RockPaperScissors.Models
{
    public class GameResult
    {
        public Player[] Players { get; set; }
        public Player WinningPlayer { get; set; }
        public string ResultReason { get; set; }
    }
}
