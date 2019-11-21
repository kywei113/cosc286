using DataStructuresCommon;
using System;

namespace LinkedList
{
    public interface I_List<T> : I_Collection<T> where T : IComparable<T>
    {
        /// <summary>
        /// Returns the element found at the given index
        /// </summary>
        /// <param name="index">The index of the item to find</param>
        /// <returns>The item at the index </returns>
        T ElementAt(int index);

        /// <summary>
        /// Returns the index where the first instance of a given item is found
        /// </summary>
        /// <param name="data">The item to find</param>
        /// <returns>The index of the item found</returns>
        int IndexOf(T data);

        /// <summary>
        /// Insert an item at a given index 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="data"></param>
        void Insert(int index, T data);

        /// <summary>
        /// Removes an element at a particular location
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        T RemoveAt(int index);

        /// <summary>
        /// Replaces the existing data at a given index with passed in data
        /// </summary>
        /// <param name="index">Index of the item to be replaced</param>
        /// <param name="data">Data item to place at the index</param>
        /// <returns>Existing item that was replaced</returns>
        T ReplaceAt(int index, T data);
    }
}
