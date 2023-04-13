namespace Utils.Math
{
    public static class Calculation
    {
        public static int ListCheck(this List<int> ints, int mod, Predicate<int> condition)
        {
            return ints.Where(i => condition(i)).Count();
        }

        public static int MinWithCheck(this List<int> ints, int mod, Predicate<int> condition)
        {
            return ints.Where(i => condition(i)).Min();
        }
    }
}