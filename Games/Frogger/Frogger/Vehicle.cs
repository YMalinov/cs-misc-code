namespace Frogger
{
    class Vehicle : MovingObject
    {
        public int Length { get; set; }

        public Vehicle(Coordinates topLeft, int speed, int length = 1)
            : base(topLeft, speed, new char[] { '█' })
        {
            this.Length = length;
        }
    }
}
