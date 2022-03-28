using MartianRobots.Models;

namespace MartianRobots.Contracts
{
    class OutputDataRobotInfo :  OutputDataModel.IActorModel
    {
        public int CoordinateOfX { get; set; }

        public int CoordinateOfY { get; set; }

        public char Direction { get; set; }

        public bool IsLostSign { get; set; }

        public string IsLostSignStringView => IsLostSign ? " LOST" : string.Empty;

        public string BaseOutputActorStringVeiw => $"{CoordinateOfX} {CoordinateOfY} {Direction}{IsLostSignStringView}";

        public OutputDataRobotInfo(Robot robot)
        {
            if (robot?.Position != null)
            {
                CoordinateOfX = robot.Position.Coordinates.X;
                CoordinateOfY = robot.Position.Coordinates.Y;
                Direction = robot.Position.DirectionCharView;
                IsLostSign = robot.IsLost;
            }
        }

        public OutputDataRobotInfo()
        {
        }
    }
}
