namespace RockPaperScissors.Models
{
    public class Player
    {
        public string Name { get; set; }
        public int Number { get; set; }
        public bool IsCpu { get; set; }
        public HandType HandType { get; set; }
        public int RoundsWon { get; set; }
    }
}
