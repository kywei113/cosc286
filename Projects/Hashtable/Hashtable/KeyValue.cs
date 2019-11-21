using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hashtable
{
    public class KeyValue<K, V>
        where K: IComparable<K>
        where V: IComparable<V>
    {
        //Store the key
        K kKey;
        V vValue;

        public KeyValue(K key, V val)
        {
            this.kKey = key;
            this.vValue = val;
        }

        public K Key
        {
            get => this.kKey;
        }

        public V Value
        {
            get => this.vValue;
        }

        //Need to override Equals so we can compare two KeyValue objects to see if they are the same
        public override bool Equals(object obj)
        {
            //Create new KeyValue by casting obj to KeyValue
            KeyValue<K, V> kv = (KeyValue<K,V>) obj;
            return this.Key.CompareTo(kv.Key) == 0; 
        }
    }
}
