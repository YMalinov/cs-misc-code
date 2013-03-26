namespace Frogger
{
    class RoadMarkings : GameObject
    {
        public RoadMarkings(Coordinates topLeft)
            : base(topLeft, new char[] { '-', ' ' })
        {
        }
    }
}