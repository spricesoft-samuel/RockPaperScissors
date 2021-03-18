using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors.Interfaces
{
    public interface IOutputDevice
    {
        Task WriteText(string text);
    }
}
