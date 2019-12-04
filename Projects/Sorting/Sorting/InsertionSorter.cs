using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting
{
    public class InsertionSorter<T> : ASorter<T> where T : IComparable<T>
    {
        public InsertionSorter(T[] array) : base(array)
        {
            
        }

        public override void Sort()
        {
            //Overall algorithm
            for(int i = 1; i < array.Length; i++)
            {
                InsertInOrder(i);
            }
        }

        private void InsertInOrder(int indexCurrent)
        {
            T unsortedElement = array[indexCurrent];
            int index = indexCurrent - 1;
            
            while(index >=0 && unsortedElement.CompareTo(array[index]) < 0)
            {
                array[indexCurrent--] = array[index--];
            }

            array[indexCurrent] = unsortedElement;

        }
    }
}
