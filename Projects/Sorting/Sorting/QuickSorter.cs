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

                //Swap out the pivot to the right-most position in the array
                Swap(pivotIndex, iEnd);

                int partitionIndex = Partition(iStart, iEnd - 1, this.array[iEnd]);     //PartitionIndex <-- Partition relative to the pivot value

                //Swap the pivot to its correct location
                if(this.array[partitionIndex].CompareTo(this.array[iEnd]) == 1)
                {
                    Swap(partitionIndex, iEnd);
                }
                
                QuickSortRec(iStart, partitionIndex);       //Recursively QuickSort the array left of the pivot
                QuickSortRec(partitionIndex + 1, iEnd);     //Recursively QuickSort the array right of the pivot
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
            while(left < right)
            {
                while (this.array[left].CompareTo(pivotValue) < 0 && left < right)
                {
                    left++;
                };

                while (this.array[right].CompareTo(pivotValue) > 0 && right > left)
                {
                    right--;
                };

                if (this.array[left].CompareTo(this.array[right]) == 1)
                {
                    Swap(left, right);
                }
            }

            return left;
        }

        protected virtual int FindPivot(int first, int last)
        {
            return last;
        }
    }
}
