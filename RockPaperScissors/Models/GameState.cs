﻿using System.Threading;

namespace RockPaperScissors.Models
{
    public class GameState
    {
        public GameFlowState FlowState { get; set; }
        public CancellationToken CancellationToken { get; set; }
        public int NumberOfPlayers { get; set; }
        public string[] PlayerNames { get; set; }
    }
}
