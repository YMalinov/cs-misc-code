namespace Frogger
{
    class Coordinates
    {
        public int Row { get; set; }
        public int Col { get; set; }

        public Coordinates(int row, int col)
        {
            this.Row = row;
            this.Col = col;
        }

        public static Coordinates operator +(Coordinates a, int col) //none of the moving objects will move up or down
        {
            return new Coordinates(a.Row, a.Col + col);
        }

        public static Coordinates operator +(Coordinates a, Coordinates b)
        {
            return new Coordinates(a.Row + b.Row, a.Col + b.Col);
        }

        public static Coordinates operator -(Coordinates a, Coordinates b)
        {
            return new Coordinates(a.Row - b.Row, a.Col - b.Col);
        }

        public override bool Equals(object obj)
        {
            Coordinates objAsMatrixCoords = obj as Coordinates;

            if (obj == null)
            {
                return false;
            }

            return objAsMatrixCoords.Row == this.Row && objAsMatrixCoords.Col == this.Col;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}