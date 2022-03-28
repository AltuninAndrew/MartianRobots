namespace MartianRobots.Models.Interfaces
{
    public interface IWorld
    {
        public bool CheckIsPointInsideInWorld(Coordinates point);

        public bool ChekIsPointContainsInDeathPoints(Coordinates point);

        public void SaveNewDeathPoint(Coordinates point);
    }
}
