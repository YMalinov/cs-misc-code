namespace Frogger
{
    class Hole : StaticObject
    {
        public Hole(Coordinates topLeft)
            : base(topLeft, new char[] { 'O' })
        {
        }
    }
}
