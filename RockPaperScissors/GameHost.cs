using Microsoft.Extensions.Hosting;
using RockPaperScissors.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    public class GameHost : IHostedService
    {
        private readonly IOutputDevice _outputDevice;

        public GameHost(IOutputDevice outputDevice)
        {
            _outputDevice = outputDevice;
        }

        public async Task StartAsync(CancellationToken cancellationToken = default)
        {
            await _outputDevice.WriteText("Testing the app runs");
        }

        public async Task StopAsync(CancellationToken cancellationToken = default)
        {
            await _outputDevice.WriteText("Testing the app has stopped");
        }
    }
}
