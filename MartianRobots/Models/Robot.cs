using System.Collections.Generic;

namespace MartianRobots.Models
{
    public class Robot
    {
        public Position Position { get; set; }

        public bool IsLost { get; set; }

        public ICollection<CommandsEnum> CommandsCollection { get; set; }

        public Robot(Position position)
        {
            Position = position;
        }
    }
}
