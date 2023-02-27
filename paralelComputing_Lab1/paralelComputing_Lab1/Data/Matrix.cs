
namespace paralelComputing_Lab1.Data
{
    internal class Matrix
    {
        private static int _matrixSize;
        private int[][] _matrix;

        public Matrix(int size = 5)
        {
            _matrixSize = size;
            _matrix = new int[size][];
            Fill();
        }
        public int[][] InnerMatrix
        {
            get { return _matrix; }
        }
        private IEnumerable<int> RandomIntsGenerator(int numberCount, int maxValue = 10, int minValue = 1)
        {
            var rnd = new Random();
            var list = new List<int>();

            for (int i = 0; i < numberCount; i++)
            {
                list.Add(rnd.Next(minValue, maxValue));
            }
            return list;
        }
        private void Fill()
        {
            for (int i = 0; i < _matrixSize; i++)
            {
                _matrix[i] = RandomIntsGenerator(_matrixSize).ToArray();
            }
        }
        public void Refill()
        {
            _matrix = new int[_matrixSize][];
            Fill();
        }

        public void SetNum(int x, int y, int num)
        {
            _matrix[x][y] = num;
        }
    }
}
