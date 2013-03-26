using System;

namespace Frogger
{
    class MovingObject : GameObject
    {
        public int Speed { get; set; }

        public MovingObject(Coordinates topLeft, int speed, char[] body)
            : base(topLeft, body)
        {
            this.Speed = speed;
        }

        public override void Update()
        {
            this.TopLeft += this.Speed;

            if (this.topLeft.Col <= -1) //teleports the moving object to the other side of the screen
            {
                this.topLeft.Col = Console.WindowWidth - 1;
            }
            else if (this.topLeft.Col >= Console.WindowWidth)
            {
                this.topLeft.Col = 0;
            }
        }
    }
}
