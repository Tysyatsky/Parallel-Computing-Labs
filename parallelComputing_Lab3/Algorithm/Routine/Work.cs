using Data.Initial;
using Data.Result;
using System.Reflection;
using Utils.Math;
using Utils.Output;
using Utils.Routine.Interfaces;

namespace Utils.Routine
{
    public class Work
    {
        private readonly IWorkable? _work;

        public Work(IWorkable? work)
        {
            _work = work;
        }

        public void DoWork(List<int> list)
        {
            int newCount = list.ListCheck(Constants.mod, Functions.isDevided);
            int minToCompare = list.MinWithCheck(Constants.mod, Functions.isDevided);

            // Printer.CurrentThreadInfo(minToCompare, newCount);

            if(_work != null) 
            {
                _work.SetCount(newCount);
                _work.SetMin(minToCompare);
            }
        }
    }
}
