using MartianRobots.Models;
using System;
using Xunit;

namespace MartianRobots.Tests
{
    public class WorldTest
    {
        [Fact]
        public void Create_World_Argument_Incorrect()
        {
            Assert.Throws<ArgumentException>(() => new World(2, new Coordinates(4, 4)));
        }

        [Fact]
        public void Check_Is_Point_Inside_In_World()
        {
            var world = new World(20, new Coordinates(5, 5));

            var point = new Coordinates(1, 1);

            Assert.True(world.CheckIsPointInsideInWorld(point));
        }

        [Fact]
        public void Check_Is_Point_Not_Inside_In_World()
        {
            var world = new World(20, new Coordinates(5, 5));

            var point = new Coordinates(-1, -1);

            Assert.True(!world.CheckIsPointInsideInWorld(point));
        }

        [Fact]
        public void Save_And_Get_New_Death_Point()
        {
            var world = new World(20, new Coordinates(5, 5));

            var point = new Coordinates(1, 1);

            world.SaveNewDeathPoint(point);

            Assert.True(world.ChekIsPointContainsInDeathPoints(point));
        }
    }
}
