using NUnit.Framework;
using System.Threading.Tasks;

namespace RockPaperScissors.Tests.Unit
{
    public class InMemoryStateChangeOutputMatrixTests
    {
        [Test]
        public async Task GetOutputForNewState_Returns_From_Resource_Started()
        {
            // Arrange
            var sut = new InMemoryStateChangeOutputMatrix();

            // Act
            var result = await sut.GetOutputForNewState(GameState.Starting);

            // Assert
            Assert.AreEqual(GameResources.WelcomeBanner, result);
        }
    }
}
