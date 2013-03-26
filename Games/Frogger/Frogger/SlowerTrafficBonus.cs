namespace Frogger
{
    class SlowerTrafficBonus : Bonus
    {
        public SlowerTrafficBonus(Coordinates topLeft)
            : base(topLeft, new char[] { '-' })
        {
        }
    }
}
