using System.Collections.Generic;

namespace MartianRobots.OutputSystem.Contracts
{
    public class OutputView
    {
        public string ErrorMessage { get; set; }

        public bool IsSuccess { get; set; }

        public ICollection<OutputViewRobotInfo> CollectionRobotsOutputInfo { get; set; }

        public OutputView(ICollection<OutputViewRobotInfo> collectionRobotsOutputInfo)
        {
            CollectionRobotsOutputInfo = collectionRobotsOutputInfo;
            IsSuccess = true;
        }

        public OutputView(string errorMessage)
        {
            IsSuccess = false;
            ErrorMessage = errorMessage;
        }


        public class OutputViewRobotInfo
        {
            public int CoordinateOfX { get; set; }

            public int CoordinateOfY { get; set; }

            public char Direction { get; set; }

            public bool IsLostSign { get; set; }

            public string IsLostSignStringView => IsLostSign ? " LOST" : string.Empty;

            public string BaseOutputStringVeiw => $"{CoordinateOfX} {CoordinateOfY} {Direction}{IsLostSignStringView}";

            public OutputViewRobotInfo(int coordinateOfX, int coordinateOfY, char direction, bool isLostSign)
            {
                CoordinateOfX = coordinateOfX;
                CoordinateOfY = coordinateOfY;
                Direction = direction;
                IsLostSign = isLostSign;
            }
        }
    }
}
