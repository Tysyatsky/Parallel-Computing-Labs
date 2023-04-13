namespace Data.Result
{
    public static class Results
    {
        public static int min = int.MaxValue;

        public static int count = 0;

        public static void Clear()
        {
            count = 0;
            min = int.MaxValue;
        }
    }
}
