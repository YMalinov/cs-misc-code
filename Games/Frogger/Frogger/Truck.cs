namespace Frogger
{
    class Truck : Vehicle
    {
        //used as a "placeholder class" - it only holds info about how big should the array of cars be
        public int Length { get; protected set; }

        public Truck(Coordinates topLeft, int speed, int length = 3)
            : base(topLeft, speed)
        {
            this.Length = length;
        }
    }
}