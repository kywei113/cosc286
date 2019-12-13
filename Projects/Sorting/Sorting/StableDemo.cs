using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting
{
    public class StableDemo : IComparable<StableDemo>
    {
        private int value;
        private int initialIndex;

        public StableDemo(int val, int index)
        {
            this.value = val;
            this.initialIndex = index;
        }


        public int CompareTo(StableDemo other)
        {
            return value.CompareTo(other.value);
        }

        public override String ToString()
        {
            return "(" + value + ", " + initialIndex + ")";
        }
    }
}
