using MartianRobots.Models.Interfaces;
using System;
using System.Collections.Generic;

namespace MartianRobots.Models
{
    public class World : IWorld
    {
        private readonly Coordinates _topLeftPoint;

        private readonly Coordinates _downRightPoint;

        private readonly Coordinates _downLeftPoint;

        private readonly HashSet<Coordinates> _deathPoints;

        public World(uint maxWorldSize, Coordinates topRightPoint)
        {
            if (topRightPoint.X > maxWorldSize || topRightPoint.Y > maxWorldSize)
            {
                throw new ArgumentException($"Coordinates of point - {topRightPoint} cannot be more than {maxWorldSize}");
            }

            _topLeftPoint = new Coordinates(0, topRightPoint.Y);
            _downRightPoint = new Coordinates(topRightPoint.X, 0);
            _downLeftPoint = new Coordinates(0, 0);

            _deathPoints = new HashSet<Coordinates>();
        }

        public bool CheckIsPointInsideInWorld(Coordinates point)
        {
            return point.X >= _downLeftPoint.X 
                && point.X <= _downRightPoint.X 
                && point.Y >= _downLeftPoint.Y 
                && point.Y <= _topLeftPoint.Y;
        }

        public bool ChekIsPointContainsInDeathPoints(Coordinates point) => _deathPoints.Contains(point);

        public void SaveNewDeathPoint(Coordinates point)
        {
            if (!_deathPoints.Contains(point))
            {
                _deathPoints.Add(point);
            }
        }
    }
}
