using MartianRobots.Configs;
using MartianRobots.InputSystem.Contracts;
using MartianRobots.Models;
using MartianRobots.OutputSystem.Contracts;
using System;
using System.Collections.Generic;

namespace MartianRobots.Interact
{
    public class RobotController : IRobotController
    {
        private ICollection<Robot> _robots;

        private World _robotWorld;

        private readonly WorldConfig _worldConfig;

        public RobotController(WorldConfig worldConfig)
        {
            _worldConfig = worldConfig ?? throw new ArgumentNullException(nameof(worldConfig));
        }

        public OutputView ExcecuteRobotsAction(InputData inputData)
        {
            var _coordinatesOfDeathPoints = new HashSet<Coordinates>();

            var initWorldError = InitWorld(inputData);
            if (!string.IsNullOrEmpty(initWorldError))
            {
                return new OutputView(initWorldError);
            }

            OutputView result = null;

            var initRobotsErrors = InitRobots(inputData);
            if (initRobotsErrors.Count > 0)
            {
                result = new OutputView(string.Join(';', initRobotsErrors));
            }

            if (_robots.Count == 0)
            {
                return result ?? new OutputView("No robots found for execution");
            }

            var robotsOutputInfo = new List<OutputView.OutputViewRobotInfo>();
            foreach (var robot in _robots)
            {
                foreach (var command in robot.CommandsCollection)
                {
                    var newRobotPotision = new Position(robot.Position.Coordinates, robot.Position.Direction).ChagePositionByCommand(command);

                    var isDeathPosition = !_robotWorld.CheckIsPointInsideInWorld(newRobotPotision.Coordinates);
                    
                    if (_coordinatesOfDeathPoints.Contains(robot.Position.Coordinates) && isDeathPosition)
                    {
                        continue;
                    }

                    if (!robot.IsLost && isDeathPosition)
                    {
                        _coordinatesOfDeathPoints.Add(robot.Position.Coordinates);
                        robot.IsLost = true;
                        break;
                    }

                    robot.Position = newRobotPotision;
                }

                var robotOutputInfo = new OutputView.OutputViewRobotInfo(robot.Position.Coordinates.X, 
                                                                         robot.Position.Coordinates.Y, 
                                                                         Position.GetDirectionChar(robot.Position.Direction), 
                                                                         robot.IsLost);
                robotsOutputInfo.Add(robotOutputInfo);
            }

            result = new OutputView(robotsOutputInfo);

            return result;
        }

        private string InitWorld(InputData inputData)
        {
            if (inputData?.WorldTopRightPointCoordinate == null)
            {
                return "World coordinates was empty";
            }

            try
            {
                _robotWorld = new World(_worldConfig, new Coordinates(inputData.WorldTopRightPointCoordinate.x, inputData.WorldTopRightPointCoordinate.y));
            }
            catch (Exception ex)
            {
                return $"System error in init robot world: {ex.Message}";
            }

            if (!(inputData.RobotsData?.Count > 0))
            {
                return "Robots data was empty";
            }

            return null;
        }

        private ICollection<string> InitRobots(InputData inputData)
        {
            _robots = new List<Robot>();
            var errors = new List<string>();
            foreach (var initRobotData in inputData.RobotsData)
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
                _robots.Add(robot);
            }

            return errors;
        }

        private (string errorMessage, Robot robot) GetRobotByInitData(int x, int y, char direct)
        {
            var robotCoordinate = new Coordinates(x, y);

            if (!_robotWorld.CheckIsPointInsideInWorld(robotCoordinate))
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

        private (string errorMessage, ICollection<CommandsEnum>) GetCommandsFromCommandString(string commandString)
        {
            commandString = commandString.Trim().ToUpper();

            var result = new List<CommandsEnum>();
            foreach(var command in commandString)
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
