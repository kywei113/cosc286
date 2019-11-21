using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hashtable
{
    class Quadratic<K, V> : A_OpenAddressing<K, V>
        where K : IComparable<K>
        where V : IComparable<V>
    {
        public Quadratic()
        {
            //Use this value for actual implementation. 0.5 prevents quadratic infinite loop
            //this.dLoadFactor = 0.5;
        }
        protected override int GetIncrement(int iAttempt, K key)
        {
            double c1 = 0.5;
            double c2 = 0.5;

            //Implement the formula for quadratic
            return (int)(c1*iAttempt + c2*Math.Pow(iAttempt,2.0));
        }
    }
}
