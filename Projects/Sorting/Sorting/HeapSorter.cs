using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting
{
    public class HeapSorter<T> : ASorter<T> where T : IComparable<T>
    {
        public HeapSorter(T[] array) : base(array)
        {
            
        }

        public override void Sort()
        {
            //Heapify the entire array
            Heapify();

            //For each element in the array starting at the last, remove from the heap.
            for(int i = array.Length - 1; i > 0; i--)
            {
                RemoveNextMax(i);
            }
        }

        /// <summary>
        /// Move the largest value to the last position of heap.
        /// Trickle top of the heap down to its logical position
        /// </summary>
        /// <param name="lastPos"></param>
        private void RemoveNextMax(int lastPos)
        {
            //Move largest to the end
            T max = array[0];
            array[0] = array[lastPos];
            array[lastPos] = max;
            //Trickle down top of heap
            TrickleDown(0, lastPos - 1);
        }


        private void Heapify()
        {
            //Gets the index of the first parent at the bottom of the heap
            int parentIndex = GetParentIndex(array.Length - 1);
            //Loop backwards from the first parent to the root
            for(int index = parentIndex; index >= 0; index--)
            {
                TrickleDown(index, array.Length - 1);
            }

        }

        /// <summary>
        /// Trickle a value down to its logical position
        /// </summary>
        /// <param name="index">Location of the element to trickle down</param>
        /// <param name="lastPos">The end of the logical heap</param>
        private void TrickleDown(int index, int lastPos)
        {
            //Get a reference to the current value to trickle down
            T current = array[index];
            int largerChildIndex = GetLeftChildIndex(index);
            bool done = false;

            //Initially, largerChildIndex points to the left child
            while (!done && largerChildIndex <= lastPos)
            {
                int rightChildIndex = GetRightChildIndex(index);
                //Check which child is larger
                if(rightChildIndex <= lastPos && array[rightChildIndex].CompareTo(array[largerChildIndex]) > 0)
                {
                    //The right child is larger
                    largerChildIndex = rightChildIndex;
                }

                //if the larger child is greater than the parent
                if(current.CompareTo(array[largerChildIndex]) < 0)
                {
                    //Move the child up to the parent location
                    array[index] = array[largerChildIndex];
                    //Set index to the larger child so we can continue trickling
                    index = largerChildIndex;
                    //Set larger child index to left child
                    largerChildIndex = GetLeftChildIndex(index);

                }
                else
                {
                    done = true;
                }

            }

            array[index] = current;
        }

        private int GetParentIndex(int index)
        {
            return (index - 1) / 2;
        }

        private int GetLeftChildIndex(int index)
        {
            return index * 2 + 1;
        }

        private int GetRightChildIndex(int index)
        {
            return index * 2 + 2;
        }
    }
}
