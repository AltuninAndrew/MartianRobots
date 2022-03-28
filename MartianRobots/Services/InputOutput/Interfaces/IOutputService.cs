using MartianRobots.Contracts;

namespace MartianRobots.Services.InputOutput.Interfaces
{
    public interface IOutputService
    {
        public void PrintMessage(OutputDataModel outputView);
    }
}
