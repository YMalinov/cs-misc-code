namespace Frogger
{
    class Vehicle : MovingObject
    {
        public Vehicle(Coordinates topLeft, int speed)
            : base(topLeft, speed, new char[] { '█' })
        {

        }
    }
}
