using MartianRobots.Models;
using MartianRobots.Models.Interfaces;
using MartianRobots.Services.Mars.Interfaces;
using System;
using System.Collections.Generic;

namespace MartianRobots.Services.Mars
{
    public class RobotService : IRobotService
    {
        private readonly IWorld _world;

        public RobotService(IWorld world)
        {
            _world = world ?? throw new ArgumentNullException(nameof(world));
        }

        public (string errorMessage, Robot robot) GetRobotByInitData(int x, int y, char direct)
        {
            var robotCoordinate = new Coordinates(x, y);

            if (!_world.CheckIsPointInsideInWorld(robotCoordinate))
            {
                return ("Coordinates of the robot do not belong to the world", null);
            }

            var robotDirection = Position.GetDirectionByChar(direct);
            if (robotDirection == null)
            {
                return ("Robot direction unrecognized", null);
            }

            var robotPosition = new Position(robotCoordinate, robotDirection.Value);

            return (null, new Robot(robotPosition));
        }

        public (ICollection<string> errors, List<Robot> robots) InitRobots(IEnumerable<(int x, int y, char direct, string instuction)> dataForInit)
        {
            var robots = new List<Robot>();
            var errors = new List<string>();
            foreach (var initRobotData in dataForInit)
            {
                var (gettingRobotError, robot) = GetRobotByInitData(initRobotData.x, initRobotData.y, initRobotData.direct);
                if (!string.IsNullOrEmpty(gettingRobotError))
                {
                    errors.Add($"Robot init error: {gettingRobotError}");
                    continue;
                }

                var (gettingRobotCommandError, commands) = GetCommandsFromCommandString(initRobotData.instuction);
                if (!string.IsNullOrEmpty(gettingRobotCommandError))
                {
                    errors.Add($"Robot init error: {gettingRobotCommandError}");
                    continue;
                }

                robot.CommandsCollection = commands;
                robots.Add(robot);
            }

            return (errors, robots);
        }

        public Robot ExecuteRobotCommands(Robot robot)
        {
            if (robot?.CommandsCollection?.Count > 0)
            {
                foreach (var command in robot.CommandsCollection)
                {
                    var newRobotPotision = new Position(robot.Position.Coordinates, robot.Position.Direction).ChagePositionByCommand(command);

                    var isDeathPosition = !_world.CheckIsPointInsideInWorld(newRobotPotision.Coordinates);

                    if (_world.ChekIsPointContainsInDeathPoints(robot.Position.Coordinates) && isDeathPosition)
                    {
                        continue;
                    }

                    if (!robot.IsLost && isDeathPosition)
                    {
                        _world.SaveNewDeathPoint(robot.Position.Coordinates);
                        robot.IsLost = true;
                        break;
                    }

                    robot.Position = newRobotPotision;
                }
            }

            return robot;
        }

        private (string errorMessage, ICollection<CommandsEnum>) GetCommandsFromCommandString(string commandString)
        {
            commandString = commandString.Trim().ToUpper();

            var result = new List<CommandsEnum>();
            foreach (var command in commandString)
            {
                var commandEnum = GetCommandByCommandChar(command);
                if (commandEnum != null)
                {
                    result.Add(commandEnum.Value);
                }
            }

            if (result.Count == 0)
            {
                return ("List of commands could not be recognized", null);
            }

            return (null, result);
        }

        private CommandsEnum? GetCommandByCommandChar(char command)
        {
            switch (command)
            {
                case 'L':
                    return CommandsEnum.Left;
                case 'R':
                    return CommandsEnum.Right;
                case 'F':
                    return CommandsEnum.Forward;
                default:
                    return null;
            }
        }

    }
}
