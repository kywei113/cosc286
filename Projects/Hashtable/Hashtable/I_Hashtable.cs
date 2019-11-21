using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hashtable
{
    /***
     * K --> Generic value representing the data type of the key
     * V --> Generic value representing the data type of the value
    */
    interface I_Hashtable<K,V> : IEnumerable<V> 
        where K: IComparable<K>
        where V: IComparable<V>
    {
        /// <summary>
        /// Returns a value from the hash table
        /// </summary>
        /// <param name="key">The key of the value to return</param>
        /// <returns>The value associated with the key</returns>
        V Get(K key);

        /// <summary>
        /// Add a key and value as a key/value pair to the hash table
        /// </summary>
        /// <param name="key">Determines the location in the hash table</param>
        /// <param name="vValue">The value to store at the location</param>
        void Add(K key, V vValue);

        /// <summary>
        /// Removes the value associated with the key/location from the hash table
        /// </summary>
        /// <param name="key">Unique identifier of the item to be removed</param>
        void Remove(K key);

        /// <summary>
        /// Removes all key/value pairs from the hash table and initializes to the default array size
        /// </summary>
        void Clear();


    }
}
