using System;
using System.Collections.Generic;
using System.Text;

namespace RockPaperScissors
{
    public enum GameFlowState
    {
        Unset = 0,
        Starting,
        WaitingForConfiguration,
        Stopping,
        Stopped
    }
}
