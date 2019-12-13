using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting
{
    public class QuickSorterMedianThree<T> : QuickSorter<T> where T : IComparable<T>
    {
        public QuickSorterMedianThree(T[] array) : base(array)
        {

        }

        protected override int FindPivot(int first, int last)
        {
            int mid = (first + last) / 2;
            if (array[first].CompareTo(array[mid]) > 0)
            {
                Swap(first, mid);
            }
            if (array[first].CompareTo(array[last]) > 0)
            {
                Swap(first, last);
            }
            if (array[mid].CompareTo(array[last]) > 0)
            {
                Swap(mid, last);
            }
            return mid;
        }
    }
}
