using MartianRobots.OutputSystem.Contracts;

namespace MartianRobots.OutputSystem
{
    public interface IOutputSystem
    {
        public void PrintMessage(OutputView outputView);
    }
}
