using MartianRobots.Contracts;

namespace MartianRobots.Controllers.Interfaces
{
    public interface IRobotController
    {
        public OutputDataModel ExcecuteRobotsAction(InputDataModel inputData);
    }
}
