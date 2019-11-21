using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hashtable
{
    internal class PrimeNumber
    {
        int iCurrent = -1;
        int[] iPrimes = { 5, 11, 19, 41, 79, 163, 317, 641, 1201, 2399, 4801, 9733, 50021, 100003, 200003, 400009, 800011, 1600033 };

        public int GetNextPrime()
        {
            return iPrimes[++iCurrent];
        }
    }
}
