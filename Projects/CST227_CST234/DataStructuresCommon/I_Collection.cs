using System;
using System.Collections.Generic;

namespace DataStructuresCommon
{
    public interface I_Collection<T> : IEnumerable<T> where T : IComparable<T>
    {
        /// <summary>
        /// Adds the given data to the collection
        /// </summary>
        /// <param name="data">Item to add</param>
        void Add(T data);

        /// <summary>
        /// Removes all items from the collection
        /// </summary>
        void Clear();

        /// <summary>
        /// Determines if the given data is in the collection
        /// </summary>
        /// <param name="data">Data item to look for</param>
        /// <returns>True if found, False if not found</returns>
        bool Contains(T data);

        /// <summary>
        /// Determines if this data structure is the same as another instance
        /// </summary>
        /// <param name="other">Data structure to compare against</param>
        /// <returns></returns>
        bool Equals(object other);

        /// <summary>
        /// Removes the first instance of a value if it exists
        /// </summary>
        /// <param name="data">Item to remove</param>
        /// <returns>True if removed, false is not removed</returns>
        bool Remove(T data);

        /// <summary>
        /// A property is used to access the number of elements in a collection
        /// A property is similar to a getter/setter
        /// </summary>
        int Count
        {
            get;
        }
    }
}
