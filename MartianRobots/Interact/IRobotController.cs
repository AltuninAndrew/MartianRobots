using MartianRobots.InputSystem.Contracts;
using MartianRobots.OutputSystem.Contracts;

namespace MartianRobots.Interact
{
    public interface IRobotController
    {
        public OutputView ExcecuteRobotsAction(InputData inputData);
    }
}
