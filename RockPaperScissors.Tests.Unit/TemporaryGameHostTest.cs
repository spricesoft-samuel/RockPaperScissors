using Moq;
using NUnit.Framework;
using RockPaperScissors.Interfaces;
using System.Threading.Tasks;

namespace RockPaperScissors.Tests.Unit
{
    public class TemporaryGameHostTest
    {
        [Test]
        public async Task TemporaryTest_StartAsync_WritesOutput()
        {
            // Arrange
            var outputDevice = new Mock<IOutputDevice>();
            var sut = new GameHost(outputDevice.Object);

            // Act
            await sut.StartAsync();

            // Assert
            outputDevice.Verify(i => i.WriteText("Testing the app runs"), Times.Once);
        }

        [Test]
        public async Task TemporaryTest_StopAsync_WritesOutput()
        {
            // Arrange
            var outputDevice = new Mock<IOutputDevice>();
            var sut = new GameHost(outputDevice.Object);

            // Act
            await sut.StopAsync();

            // Assert
            outputDevice.Verify(i => i.WriteText("Testing the app has stopped"), Times.Once);
        }
    }
}