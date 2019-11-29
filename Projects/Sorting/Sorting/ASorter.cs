using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting
{
    public abstract class ASorter<T> where T : IComparable<T>
    {
        protected T[] array;
        
        public ASorter(T[] array)
        {
            this.array = array;
        }

        public abstract void Sort();

        protected void Swap(int first, int second)
        {
            T swap = array[first];

            array[first] = array[second];
            array[second] = swap;
        }

        public int Length
        {
            get
            {
                return array.Length;
            }
        }

        //Override the [] operator
        public T this [int index]
        {
            get
            {
                return array[index];
            }
            set
            {
                array[index] = value;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("[");

            foreach (T tValue in array)
            {
                sb.Append(tValue);
                sb.Append(", ");
            }

            if(sb.Length > 1)
            {
                sb.Remove(sb.Length - 2, 2);
            }

            sb.Append("]");
            return sb.ToString();
        }
    }
}
