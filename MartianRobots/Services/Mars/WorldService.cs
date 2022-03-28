using MartianRobots.Models;
using MartianRobots.Models.Interfaces;
using MartianRobots.Services.Mars.Interfaces;
using System;

namespace MartianRobots.Services.Mars
{
    public class WorldService : IWorldService
    {
        private readonly uint _maxWorldSize;

        public WorldService(uint maxWorldSize)
        {
            _maxWorldSize = maxWorldSize;
        }

        public (IWorld world, string errorMessage) GetWorld(Coordinates topRightCoordinates, uint? maxWorldSize = null)
        {
            string message = null;

            World world;
            try
            {
                world = new World(maxWorldSize ?? _maxWorldSize, topRightCoordinates);
            }
            catch (Exception ex)
            {
                return (null, $"System error in init robot world: {ex.Message}");
            }

            return (world, message);
        }
    }
}
