namespace Frogger
{
    class Frog : GameObject
    {
        public Frog(Coordinates topLeft)
            : base(topLeft, new char[] { '&' })
        {
        }

        internal void MoveLeft()
        {
            this.topLeft.Col--;
        }

        internal void MoveRight()
        {
            this.topLeft.Col++;
        }

        internal void MoveUp()
        {
            this.topLeft.Row -= 2; //-2, because we want to jump over the road surface markings
        }

        internal void MoveDown()
        {
            this.topLeft.Row += 2;
        }
    }
}
