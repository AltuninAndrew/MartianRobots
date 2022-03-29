using MartianRobots.Models;
using MartianRobots.Services.Mars;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MartianRobots.Tests.ServicesTests
{
    public class RobotServiceTests
    {
        [Fact]
        public void Get_Robot_By_Init_Data()
        {
            var robotService = new RobotService(new World(50, new Coordinates(5, 5)));
            
            var robot = robotService.GetRobotByInitData(2, 2, 'W').robot;

            var expectedRobot = new Robot(new Position(new Coordinates(2, 2), Position.DirectionEnum.West));

            Assert.Equal(robot.Position.Direction, expectedRobot.Position.Direction);
            Assert.Equal(robot.Position.Coordinates, expectedRobot.Position.Coordinates);
            Assert.Equal(robot.CommandsCollection, expectedRobot.CommandsCollection);
            Assert.Equal(robot.IsLost, expectedRobot.IsLost);
        }

        [Fact]
        public void Init_Robots()
        {
            var robotService = new RobotService(new World(50, new Coordinates(5, 5)));

            var robot = robotService.InitRobots(new List<(int, int, char, string)>() { (2, 2, 'W', "FRL") }).robots[0];

            var expectedRobot = new Robot(new Position(new Coordinates(2, 2), Position.DirectionEnum.West))
            {
                CommandsCollection = new List<CommandsEnum>()
                {
                    CommandsEnum.Forward,
                    CommandsEnum.Right,
                    CommandsEnum.Left
                }
            };

            Assert.Equal(robot.Position.Direction, expectedRobot.Position.Direction);
            Assert.Equal(robot.Position.Coordinates, expectedRobot.Position.Coordinates);
            Assert.Equal(robot.CommandsCollection, expectedRobot.CommandsCollection);
            Assert.Equal(robot.IsLost, expectedRobot.IsLost);
        }

        [Fact]
        public void Execute_Robot_Commands()
        {
            var robot = new Robot(new Position(new Coordinates(1, 1), Position.DirectionEnum.East))
            {
                CommandsCollection = new List<CommandsEnum>()
                {
                    CommandsEnum.Right,
                    CommandsEnum.Forward,
                    CommandsEnum.Right,
                    CommandsEnum.Forward,
                    CommandsEnum.Right,
                    CommandsEnum.Forward,
                    CommandsEnum.Right,
                    CommandsEnum.Forward,
                }
            };

            var robotService = new RobotService(new World(50, new Coordinates(5, 3)));

            var changedRobot = robotService.ExecuteRobotCommands(robot);

            var expectedRobot = new Robot(new Position(new Coordinates(1, 1), Position.DirectionEnum.East));

            Assert.Equal(changedRobot.Position.Direction, expectedRobot.Position.Direction);
            Assert.Equal(changedRobot.Position.Coordinates, expectedRobot.Position.Coordinates);
            Assert.Equal(changedRobot.IsLost, expectedRobot.IsLost);
        }
    }
}
