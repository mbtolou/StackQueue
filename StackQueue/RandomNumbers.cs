namespace StackQueue
{
    /// <summary>
    /// این کلاس برای تولید اعداد تصادفی است
    /// </summary>
    internal static class RandomNumbers
    {
        private static System.Random r;

        internal static int NextNumber()
        {
            if (r == null)
                Seed();

            return r.Next();
        }

        internal static int NextNumber(int ceiling)
        {
            if (r == null)
                Seed();

            int v = 0;

            while (v == 0)
                v = r.Next(ceiling);

            return v;
        }

        internal static void Seed()
        {
            r = new System.Random();
        }

        internal static void Seed(int seed)
        {
            r = new System.Random(seed);
        }
    }
}