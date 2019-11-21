using DataStructuresCommon;
using System;
using System.Collections.Generic;

namespace LinkedList
{
    public abstract class A_List<T> : A_Collection<T>, I_List<T> where T : IComparable<T>
    {
        #region Abstract Methods
        public abstract void Insert(int index, T data);

        public abstract T RemoveAt(int index);

        public abstract T ReplaceAt(int index, T data);
        #endregion


        public T ElementAt(int index)
        {
            T tOriginal = default(T);

            //Count the number of times we looped
            int count = 0;

            //Bounds check
            if (index < 0 || index >= this.Count)
            {
                throw new IndexOutOfRangeException("Invalid index - " + index);
            }

            //Get an enumerator
            IEnumerator<T> myEnum = GetEnumerator();
            myEnum.Reset();

            //Loop while there are more data items and not at the given index
            while (myEnum.MoveNext() && count != index)
            {
                count++;
            }

            //Get the current value from the enumerator
            tOriginal = myEnum.Current;

            return tOriginal;

        }

        public int IndexOf(T data)
        {
            int nIndex = 0;

            IEnumerator<T> myEnum = GetEnumerator();
            myEnum.Reset();

            while (myEnum.MoveNext() && !myEnum.Current.Equals(data))
            {
                nIndex++;
            }

            if(!myEnum.MoveNext() && !myEnum.Current.Equals(data))
            {
                nIndex = -1;
            }

            return nIndex;
        }

    }
}
