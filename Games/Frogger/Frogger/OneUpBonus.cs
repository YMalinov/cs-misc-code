namespace Frogger
{
    class OneUpBonus : Bonus
    {
        public OneUpBonus(Coordinates topLeft)
            : base(topLeft, new char[] { '+' })
        {
        }
    }
}
