using MartianRobots.Models;
using MartianRobots.Models.Interfaces;

namespace MartianRobots.Services.Mars.Interfaces
{
    public interface IWorldService
    {
        public (IWorld world, string errorMessage) GetWorld(Coordinates topRightCoordinates, uint? maxWorldSize = null);
    }
}
