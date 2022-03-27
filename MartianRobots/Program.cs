using System;
using System.Collections.Generic;
using MartianRobots.Configs;
using MartianRobots.InputSystem.Contracts;
using MartianRobots.Interact;

namespace MartianRobots
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World! ");

            var config = new WorldConfig();

            var inputSystem = new InputSystem.InputSystem(config.NumsOfRobotsInWorld);

            var inputData = new InputData()
            {
                RobotsData = new List<(int x, int y, char direct, string instuction)>()
                {
                    (1, 1, 'E', "RFRFRFRF"),
                    (3, 2, 'N', "FRRFLLFFRRFLL"),
                    (0, 3, 'W', "LLFFFLFLFL"),
                },
                WorldTopRightPointCoordinate = (5, 3)
            };
            //var inputData = inputSystem.GetInputData();

            var robotController = new RobotController(config);

            var outputInfo = robotController.ExcecuteRobotsAction(inputData);

            var outputSystem = new OutputSystem.OutputSystem();

            outputSystem.PrintMessage(outputInfo);

            Console.ReadKey();
        }
    }
}
