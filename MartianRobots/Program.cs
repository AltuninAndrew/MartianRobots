using MartianRobots.Configs;
using MartianRobots.Contracts;
using MartianRobots.Controllers;
using MartianRobots.Controllers.Interfaces;
using MartianRobots.Services.InputOutput;
using MartianRobots.Services.InputOutput.Interfaces;
using System;
using System.Collections.Generic;

namespace MartianRobots
{
    class Program
    {
        static void Main()
        {
            var config = new WorldConfig();

            var inputService = new InputService(config.NumsOfRobotsInWorld);
            var outputService = new OutputService();
            var robotsController = new MartianRobotsController(config);

            TestWithTemplate(robotsController, outputService);
            TestByConsole(robotsController, inputService, outputService);

            Console.ReadKey();
        }

        static void TestWithTemplate(IRobotController robotsController, IOutputService outputService)
        {
            var inputData = new InputDataModel
            {
                WorldTopRightPointCoordinate = (5, 3),
                RobotsData = new List<(int x, int y, char direct, string instuction)>()
                {
                    (1, 1, 'E', "RFRFRFRF"),
                    (3, 2, 'N', "FRRFLLFFRRFLL"),
                    (0, 3, 'W', "LLFFFLFLFL"),
                }
            };

            Console.WriteLine($"Test by template:\nWorld: {inputData.WorldTopRightPointCoordinate} \nRobots: {string.Join("; ", inputData.RobotsData)}");

            Console.WriteLine("\nResul: ");
            outputService.PrintMessage(robotsController.ExcecuteRobotsAction(inputData));
            Console.WriteLine("\nDone. Test by console: ");
        }

        static void TestByConsole(IRobotController robotsController, IInputService inputService, IOutputService outputService)
        {
            var inputData = inputService.GetInputData();
            outputService.PrintMessage(robotsController.ExcecuteRobotsAction(inputData));
            Console.WriteLine("\nDone.");
        }
    }
}
