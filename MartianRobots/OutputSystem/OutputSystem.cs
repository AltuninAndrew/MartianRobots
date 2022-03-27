using MartianRobots.OutputSystem.Contracts;
using System;

namespace MartianRobots.OutputSystem
{
    public class OutputSystem : IOutputSystem
    {
        public void PrintMessage(OutputView outputView)
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
                        var outputString = info.BaseOutputStringVeiw;
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
