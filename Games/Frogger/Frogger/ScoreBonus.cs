namespace Frogger
{
    class ScoreBonus : Bonus
    {
        public ScoreBonus(Coordinates topLeft)
            : base(topLeft, new char[] { 'S' })
        {
        }
    }
}
