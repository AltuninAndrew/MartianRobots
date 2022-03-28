using MartianRobots.Configs;
using MartianRobots.Contracts;
using MartianRobots.Controllers.Interfaces;
using MartianRobots.Models;
using MartianRobots.Services.Mars;
using MartianRobots.Services.Mars.Interfaces;
using System;
using System.Collections.Generic;

namespace MartianRobots.Controllers
{
    public class MartianRobotsController : IRobotController
    {
        private readonly IWorldService _worldService;

        private IRobotService _robotService;

        public MartianRobotsController(WorldConfig worldConfig)
        {
            var config = worldConfig ?? throw new ArgumentNullException(nameof(worldConfig));

            _worldService = new WorldService(config.MaxWorldLength);
        }

        public OutputDataModel ExcecuteRobotsAction(InputDataModel inputData)
        {
            if (inputData == null)
            {
                return new OutputDataModel("Input data was null");
            }

            if (!(inputData.RobotsData?.Count > 0))
            {
                return new OutputDataModel("Input robots data was empty");
            }

            var newWorld = _worldService.GetWorld(new Coordinates(inputData.WorldTopRightPointCoordinate.x, inputData.WorldTopRightPointCoordinate.y));
            if (!string.IsNullOrEmpty(newWorld.errorMessage))
            {
                return new OutputDataModel(newWorld.errorMessage);
            }

            _robotService = new RobotService(newWorld.world);

            var (errors, robots) = _robotService.InitRobots(inputData.RobotsData);
            if (errors?.Count > 0)
            {
                return new OutputDataModel(string.Join(';', errors));
            }

            if (!(robots?.Count > 0))
            {
                return new OutputDataModel("Failed to create robots");
            }

            var robotsOutputInfo = new List<OutputDataModel.IActorModel>();

            for (var i = 0; i < robots.Count; i++)
            {
                robots[i] = _robotService.ExecuteRobotCommands(robots[i]);

                var robotOutputInfo = new OutputDataRobotInfo(robots[i]);
                robotsOutputInfo.Add(robotOutputInfo);
            }

            return new OutputDataModel(robotsOutputInfo);
        }
    }
}
