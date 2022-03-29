using MartianRobots.Models;
using MartianRobots.Services.Mars;
using Xunit;

namespace MartianRobots.Tests.ServicesTests
{
    public class WorldServiceTests
    {
        [Fact]
        public void Getting_World()
        {
            var worldService = new WorldService(10);

            Assert.NotNull(worldService.GetWorld(new Coordinates(1, 1)).world);
        }

        [Fact]
        public void Getting_World_With_Error()
        {
            var worldService = new WorldService(10);
            var (_, errorMessage) = worldService.GetWorld(new Coordinates(50, 50));

            Assert.NotNull(errorMessage);
        }
    }
}
