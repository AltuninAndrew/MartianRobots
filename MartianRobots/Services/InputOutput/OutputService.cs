using MartianRobots.Contracts;
using MartianRobots.Services.InputOutput.Interfaces;
using System;

namespace MartianRobots.Services.InputOutput
{
    public class OutputService : IOutputService
    {
        public void PrintMessage(OutputDataModel outputView)
        {
            if (outputView != null)
            {
                if (!outputView.IsSuccess)
                {
                    Console.WriteLine(outputView.ErrorMessage);
                }
                else if (outputView.CollectionRobotsOutputInfo != null)
                {
                    foreach (var info in outputView.CollectionRobotsOutputInfo)
                    {
                        var outputString = info.BaseOutputActorStringVeiw;
                        if (!string.IsNullOrEmpty(outputString))
                        {
                            Console.WriteLine(outputString);
                        }
                    }
                }
            }
        }
    }
}
