using System;
using System.Collections.Generic;
using System.Linq;
using MartianRobots.InputSystem.Contracts;

namespace MartianRobots.InputSystem
{
    public class InputSystem : IInputSystem
    {
        private readonly uint _numsOfRobotsInWorld;

        public InputSystem(uint numsOfRobotsInWorld)
        {
            if (numsOfRobotsInWorld == 0)
            {
                throw new ArgumentException($"{numsOfRobotsInWorld} was 0");
            }

            _numsOfRobotsInWorld = numsOfRobotsInWorld;
        }

        public InputData GetInputData()
        {
            var worldData = GetWorldDataTopRightPoint();

            var robotsData = new List<(int x, int y, char direct, string instuction)>();
            for (var i = 0; i < _numsOfRobotsInWorld; i++)
            {
                var (x, y, direction) = GetRobotCoordinates();

                var instruction = Console.ReadLine();
                if (string.IsNullOrEmpty(instruction))
                {
                    throw new ArgumentNullException("Robot instruction input string was null or empty");
                }

                robotsData.Add((x, y, direction, instruction));
            }

            return new InputData
            {
                WorldTopRightPointCoordinate = worldData,
                RobotsData = robotsData
            };
        }

        private (int x, int y) GetWorldDataTopRightPoint()
        {
            var inputString = Console.ReadLine();

            if (string.IsNullOrEmpty(inputString))
            {
                throw new ArgumentNullException("World data input string was null or empty");
            }

            inputString = inputString.Trim();

            var splittedInput = inputString.Split(' ');
            if (splittedInput.Length > 2)
            {
                throw new ArgumentException("World data input was contained more than two values");
            }

            var xPoint = GetIntValueFromInputString(splittedInput.ElementAt(0));
            var yPoint = GetIntValueFromInputString(splittedInput.ElementAt(1));

            return (xPoint, yPoint);
        }

        private (int x, int y, char direction) GetRobotCoordinates()
        {
            var inputString = Console.ReadLine();

            if (string.IsNullOrEmpty(inputString))
            {
                throw new ArgumentNullException("Robot coordinate and direction input string was null or empty");
            }

            var splittedInput = inputString.Split(' ');
            if (splittedInput.Length > 3)
            {
                throw new ArgumentException("Robot coordinate and direction input was contained more than three values");
            }

            var xPointRobot = GetIntValueFromInputString(splittedInput.ElementAt(0));
            var yPointRobot = GetIntValueFromInputString(splittedInput.ElementAt(1));

            var direction = splittedInput.ElementAt(2).ToUpper().ElementAt(0);

            return (xPointRobot, yPointRobot, direction);
        }

        private int GetIntValueFromInputString(string inputString)
        {
            if (int.TryParse(inputString, out var intValue))
            {
                return intValue;
            }
            else
            {
                throw new ArgumentException("X world point not INT");
            }
        }
    }
}
