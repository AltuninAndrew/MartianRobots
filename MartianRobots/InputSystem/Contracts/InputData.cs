using System.Collections.Generic;

namespace MartianRobots.InputSystem.Contracts
{
    public class InputData
    {
        public (int x, int y) WorldTopRightPointCoordinate { get; set; }

        public ICollection<(int x, int y, char direct, string instuction)> RobotsData { get; set; }
    }
}
