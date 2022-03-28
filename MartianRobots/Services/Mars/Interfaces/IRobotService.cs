using MartianRobots.Models;
using System.Collections.Generic;

namespace MartianRobots.Services.Mars.Interfaces
{
    public interface IRobotService
    {
        public (ICollection<string> errors, List<Robot> robots) InitRobots(IEnumerable<(int x, int y, char direct, string instuction)> dataForInit);

        public (string errorMessage, Robot robot) GetRobotByInitData(int x, int y, char direct);

        public Robot ExecuteRobotCommands(Robot robot);
    }
}
