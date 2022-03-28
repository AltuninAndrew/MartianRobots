using System.Collections.Generic;

namespace MartianRobots.Contracts
{
    public class InputDataModel
    {
        public (int x, int y) WorldTopRightPointCoordinate { get; set; }

        public ICollection<(int x, int y, char direct, string instuction)> RobotsData { get; set; }
    }
}
