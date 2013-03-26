namespace Frogger
{
    static class Converter
    {
        public static int LanesToHeight(int lanes)
        {
            return lanes * 2 + 4;
        }

        public static int HeightToLanes(int height)
        {
            return (height - 4) / 2;
        }
    }
}