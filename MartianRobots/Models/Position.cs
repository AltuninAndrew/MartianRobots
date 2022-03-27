namespace MartianRobots.Models
{
    public class Position
    {
        public Coordinates Coordinates { get; set; }

        public DirectionEnum Direction { get; set; }

        public Position(Coordinates coordinates, DirectionEnum direction)
        {
            Coordinates = coordinates;
            Direction = direction;
        }

        public enum DirectionEnum
        {
            North = 0,
            South = 180,
            East = 90,
            West = 270
        }

        public Position ChagePositionByCommand(CommandsEnum command)
        {
            if (command == CommandsEnum.Left)
            {
                Direction = GetDirectionTurnLeft90(Direction);
            }
            
            if (command == CommandsEnum.Right)
            {
                Direction = GetDirectionTurnRigh90(Direction);
            } 
            
            if (command == CommandsEnum.Forward)
            {
                switch (Direction)
                {
                    case DirectionEnum.North:
                        Coordinates = new Coordinates(Coordinates.X, Coordinates.Y + 1);
                        break;
                    case DirectionEnum.South:
                        Coordinates = new Coordinates(Coordinates.X, Coordinates.Y - 1);
                        break;
                    case DirectionEnum.West:
                        Coordinates = new Coordinates(Coordinates.X - 1, Coordinates.Y);
                        break;
                    case DirectionEnum.East:
                        Coordinates = new Coordinates(Coordinates.X + 1, Coordinates.Y);
                        break;
                }
            }

            return this;
        }

        public static DirectionEnum? GetDirectionByChar(char direction)
        {
            return direction switch
            {
                'N' => DirectionEnum.North,
                'S' => DirectionEnum.South,
                'E' => DirectionEnum.East,
                'W' => DirectionEnum.West,
                _ => null,
            };
        }

        public static char GetDirectionChar(DirectionEnum direction)
        {
            return direction switch
            {
                DirectionEnum.North => 'N',
                DirectionEnum.South => 'S',
                DirectionEnum.East => 'E',
                DirectionEnum.West => 'W',
                _ => ' ',
            };
        }

        private static DirectionEnum GetDirectionTurnRigh90(DirectionEnum currentDirection)
        {
            var turnedDirection = currentDirection + 90;

            if ((int)turnedDirection == 360)
            {
                turnedDirection = DirectionEnum.North;
            }

            return turnedDirection;
        }

        private static DirectionEnum GetDirectionTurnLeft90(DirectionEnum currentDirection)
        {
            var turnedDirection = currentDirection - 90;

            if ((int)turnedDirection == -90)
            {
                turnedDirection = DirectionEnum.West;
            }

            return turnedDirection;
        }
    }
}
