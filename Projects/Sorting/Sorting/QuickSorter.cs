using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting
{
    public class QuickSorter<T> : ASorter<T> where T : IComparable<T>
    {
        public QuickSorter(T[] array) : base(array)
        {

        }

        public override void Sort()
        {
            QuickSortRec(0, this.array.Length - 1);
        }

        private void QuickSortRec(int iStart, int iEnd)
        {
            //Base Case - 1 Element, Do Nothing
            
            //Recursive Case - 2 or more elements
            if(iEnd - iStart > 0)
            {
                int pivotIndex = FindPivot(iStart, iEnd);   //PivotLocation <-- FindPivot

                
                Swap(pivotIndex, iEnd);             //Swap out the pivot to the right-most position in the array

                int partitionIndex = Partition(iStart - 1, iEnd, this.array[iEnd]);     //PartitionIndex <-- Partition relative to the pivot value

                
                Swap(partitionIndex, iEnd);                 //Swap the pivot to its correct location

                if(iEnd - iStart > 1000)
                {
                    Parallel.Invoke(
                        () => QuickSortRec(iStart, partitionIndex - 1),   //Recursively QuickSort the array left of the pivot
                        () => QuickSortRec(partitionIndex + 1, iEnd)     //Recursively QuickSort the array right of the pivot
                    );
                }
                else
                {
                    QuickSortRec(iStart, partitionIndex - 1);
                    QuickSortRec(partitionIndex + 1, iEnd);
                }






            }
        }

        /// <summary>
        /// Partitions the array defined by the left and right indices.
        /// </summary>
        /// <param name="left">Start location of the array's left pointer</param>
        /// <param name="right">Start location of the array's right pointer</param>
        /// <param name="pivotValue">Value being used as the pivot</param>
        /// <returns>The partition index where lefet pointer ends</returns>
        private int Partition(int left, int right, T pivotValue)
        {
            do
            {
                while (array[++left].CompareTo(pivotValue) < 0) ;
                while (right > left && array[--right].CompareTo(pivotValue) > 0) ;
                Swap(left, right);
            } while (left < right);

            return left;
        }

        protected virtual int FindPivot(int first, int last)
        {
            return last;
        }
    }
}
