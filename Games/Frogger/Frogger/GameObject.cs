namespace Frogger
{
    abstract class GameObject
    {
        protected Coordinates topLeft;

        public Coordinates TopLeft
        {
            get
            {
                return new Coordinates(topLeft.Row, topLeft.Col);
            }

            set
            {
                this.topLeft = new Coordinates(value.Row, value.Col);
            }
        }

        protected char[] body;

        protected GameObject(Coordinates topLeft, char[] body)
        {
            this.TopLeft = topLeft;
            this.body = (char[])body.Clone();
        }

        public virtual void Update()
        {
        }

        public Coordinates GetTopLeft()
        {
            return this.topLeft;
        }

        public char[] GetImage()
        {
            return this.body;
        }
    }
}
