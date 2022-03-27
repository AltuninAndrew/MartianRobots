using MartianRobots.Configs;
using System;

namespace MartianRobots.Models
{
    public class World
    {
        public Coordinates TopRightPoint { get; }

        public Coordinates TopLeftPoint { get; }

        public Coordinates DownRightPoint { get; }

        public Coordinates DownLeftPoint { get; }

        private readonly WorldConfig _worldConfig;

        public World(WorldConfig worldConfig, Coordinates topRightPoint)
        {
            _worldConfig = worldConfig ?? throw new ArgumentNullException(nameof(worldConfig));

            if (topRightPoint.X > _worldConfig.MaxWorldLength || topRightPoint.Y > _worldConfig.MaxWorldLength)
            {
                throw new ArgumentException($"Coordinates of point - {topRightPoint} cannot be more than {_worldConfig.MaxWorldLength}");
            }

            TopRightPoint = topRightPoint;
            TopLeftPoint = new Coordinates(0, topRightPoint.Y);
            DownRightPoint = new Coordinates(topRightPoint.X, 0);
            DownLeftPoint = new Coordinates(0, 0);
        }

        public bool CheckIsPointInsideInWorld(Coordinates point)
        {
            return point.X >= DownLeftPoint.X 
                && point.X <= DownRightPoint.X 
                && point.Y >= DownLeftPoint.Y 
                && point.Y <= TopLeftPoint.Y;
        }
    }
}
