using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hashtable
{
    public abstract class A_Hashtable<K, V> : I_Hashtable<K, V>
        where K : IComparable<K>
        where V : IComparable<V>
    {



        #region Attributes
        /*
            object[] can be substituted with a generic instead of object. May introduce its own challenges
            In the case of chaining, this array will store a secondary data structure.
            In our case, we'll use ArrayList
            In the case of open-addressing (probing algorithms) the array will store key-value pair objects
         */
        protected object[] oDataArray;

        //The number of elements in the array
        protected int iCount;

        /*
            Load factor - used to set the maximum percentage full that we will allow the array to fill
            The factor 0.72 is used by Microsoft for their hashtable
        */
        protected double dLoadFactor = 0.72;

        /*
            Collision Count - used for statistical purposes. Not normally found
         */
        protected int iNumCollisions = 0;
        #endregion


        #region Properties
        public int Count { get => iCount; }
        public int NumCollisions { get => iNumCollisions; }

        public int HTSize
        {
            get
            {
                return oDataArray.Length;
            }
            //Equivalent to get => oDataArray.Length;
        }
        #endregion

        #region Helper Methods
        protected int HashFunction(K key)
        {
            /*
             * All objects in C# have a GetHashCode() function
             * Note that later on, our key object can override the GetHashCode method
             */
            return Math.Abs(key.GetHashCode() % HTSize);
        }
        #endregion

        #region I_Hashtable Implementation
        public void Clear()
        {
            throw new NotImplementedException();
        }

        public abstract void Add(K key, V vValue);

        public abstract V Get(K key);

        public abstract void Remove(K key);
        #endregion

        //Implement in child class depending on the specific implementation
        public abstract IEnumerator<V> GetEnumerator();

        //Microsoft method for non-generic enumerator
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
