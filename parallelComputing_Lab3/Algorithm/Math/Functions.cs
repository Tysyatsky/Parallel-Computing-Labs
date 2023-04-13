using Data.Initial;

namespace Utils.Math
{
    public static class Functions
    {
        public static Predicate<int> isDevided = i => i % Constants.mod == 0;
    }
}
