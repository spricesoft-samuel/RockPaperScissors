using Microsoft.Extensions.Hosting;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
[assembly: InternalsVisibleTo("RockPaperScissors.Tests.Unit")]

namespace RockPaperScissors
{
    class Program
    {
        static  Task Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();

            host.RunAsync();
            return Task.CompletedTask;
        }

        static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args).ConfigureServices(ServiceRegistry.Register);
        }
    }
}
