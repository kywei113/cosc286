using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DataStructuresCommon
{
    public abstract class A_Collection<T> : I_Collection<T> where T : IComparable<T>
    {
        #region Abstract Methods
        public abstract void Add(T data);

        public abstract void Clear();

        public abstract bool Remove(T data);

        public abstract IEnumerator<T> GetEnumerator();
        #endregion

        //Recall that Count is a property, a C# construct similar to a getter/setter
        //Note that the following implementation of Count is probably not very efficient
        //Therefore, we want the ability to override this property in a child implementation
        //The keyword "virtual" allows tihs to occur.
        public virtual int Count
        {
            get
            {
                int count = 0;
                //The foreach statement works for collections that implement the IEnumerable interface
                //The foreach will automatically call GetEnumerator and then use it to iterate through the collection
                foreach (T item in this)
                {
                    count++;
                }
                return count;
            }
        }

        public bool Contains(T data)
        {
            bool bFound = false;

            //Get an instance of the enumerator
            IEnumerator<T> myEnum = GetEnumerator();

            //Begin enumeration at the start of the collection
            myEnum.Reset();

            //Loop through the data until the requested item is found, or we reach the end of the collection
            while (!bFound && myEnum.MoveNext())
            {
                //if(myEnum.Current.Equals(data).Equals(data))
                //{
                //    bFound = myEnum.Current.Equals(data);
                //}
                bFound = myEnum.Current.Equals(data);
            }

            return bFound;
        }

        //Override the implementation of ToString.  Typically, this method would not
        //be part of a data structure, at least not this implementation where all data items
        //are appended to the string.  Could get really long ......
        public override string ToString()
        {
            StringBuilder result = new StringBuilder("[");
            string sep = ", ";

            foreach (T item in this)
            {
                result.Append(item + sep);
            }

            if (Count > 0)
            {
                result.Remove(result.Length - sep.Length, sep.Length);
            }

            result.Append("]");

            return result.ToString();
        }

        /// <summary>
        /// Non-generic version of GetEnumerator
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
