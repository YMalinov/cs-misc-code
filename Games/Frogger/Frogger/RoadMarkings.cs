namespace Frogger
{
    class RoadMarkings : PassableObject
    {
        public RoadMarkings(Coordinates topLeft)
            : base(topLeft, new char[] { '-', ' ' })
        {
        }
    }
}