namespace Data.Initial
{
    public class RandomizedData
    {
        private static Random _random = new Random();
        public static List<int> GetRandomizedData(int count)
        {
            List<int> data = new List<int>(count);

            for (int i = 0; i < count; i++)
            {
                data.Add(_random.Next(1, 10000000));
            }

            return data;
        }
    }
}
