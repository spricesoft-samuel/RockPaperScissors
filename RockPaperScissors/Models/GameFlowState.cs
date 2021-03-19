namespace RockPaperScissors.Models
{
    public enum GameFlowState
    {
        Unset = 0,
        Starting,
        ChooseNumberOfPlayers,
        EnterPlayerNames,
        ChooseHand,
        Stopping,
        Stopped,
    }
}
