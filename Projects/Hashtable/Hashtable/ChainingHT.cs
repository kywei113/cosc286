using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;

namespace Hashtable
{
    class ChainingHT<K, V> : A_Hashtable<K, V>
        where K : IComparable<K>
        where V : IComparable<V>
    {
        private int iBucketCount = 0;
        //initial table size
        private const int iInitialSize = 2;

        public int BucketCount { get => iBucketCount; set => iBucketCount = value; }

        public ChainingHT()
        {
            //dLoadFactor = 0.5;
            oDataArray = new object[iInitialSize];
        }

        public override void Add(K key, V vValue)
        {
            //The current location we are trying to add into by hashing the key
            int iCurrentLocation = HashFunction(key);

            //Wrap the key and value in a KeyValue object
            KeyValue<K, V> kvNew = new KeyValue<K, V>(key, vValue);

            if(oDataArray[iCurrentLocation] == null)
            {
                oDataArray[iCurrentLocation] = new ArrayList();
                //Increment the count
                iBucketCount++;
                iNumCollisions++;
            }

            if (oDataArray[iCurrentLocation].GetType() == typeof(ArrayList))
            {
                //Check the current key-value to make sure it's not the same as the key-value being added
                ArrayList al = (ArrayList)oDataArray[iCurrentLocation];

                if (al.Contains(kvNew))
                {
                    throw new ApplicationException("Item already exists");
                }
                else
                {
                    //Add the key-value to the table
                    al.Add(kvNew);
                    iCount++;
                }
            }

            if (IsOverloaded())
            {
                ExpandHashTable();
            }

        }

        private void ExpandHashTable()
        {
            //Create a reference to the existing hash table array
            object[] oOldArray = oDataArray;

            //Assign a new array to the hash table
            oDataArray = new Object[this.HTSize * 2];

            //Reset attributes
            iCount = 0;
            iNumCollisions = 0;
            BucketCount = 0;

            //Add the items from the old array to the new array
            foreach (object ob in oOldArray)
            {
                if (ob != null)
                {
                    foreach(KeyValue<K,V> kv in (ArrayList) ob)
                    {
                        KeyValue<K, V> kvOB = kv;
                        this.Add(kvOB.Key, kvOB.Value);
                    }

                }
            }
        }

        private bool IsOverloaded()
        {
            return iBucketCount / (double)HTSize > dLoadFactor;
        }

        public override V Get(K key)
        {
            V vReturn = default(V);
            
            KeyValue<K, V> kvFind = new KeyValue<K, V>(key, default(V));
            int iIndexOfValue = -1;
            int iHash = HashFunction(key);
            ArrayList alCurrent = (ArrayList) oDataArray[iHash];

            if (alCurrent != null)
            {
                iIndexOfValue = alCurrent.IndexOf(kvFind);

                if(iIndexOfValue >= 0)
                {
                    vReturn = ((KeyValue<K,V>) alCurrent[iIndexOfValue]).Value;
                }
            }

            return vReturn;

            //bool bFound = false;
            //int iCurrent = 0;
            //while(!bFound)
            //{
            //    if(oDataArray[iCurrent] != null && oDataArray[iCurrent].GetType() == typeof (ArrayList))
            //    {
            //        foreach(KeyValue<K,V> kv in (ArrayList) oDataArray[iCurrent])
            //        {
            //            if (kv.Equals(kvFind))
            //            {
            //                kvFind = kv;
            //                bFound = true;
            //                break;
            //            }
            //        }
            //    }

            //    iCurrent++;
            //}

            //return kvFind.Value;

        }

        public override IEnumerator<V> GetEnumerator()
        {
            return new ChainEnumerator(this);
        }

        private class ChainEnumerator : IEnumerator<V>
        {
            private ChainingHT<K, V> parent = null;
            private int iCurrent = -1;
            //private int iArrayIndex = -1;
            private KeyValue<K, V> kvCurrent = null;
            private Queue<KeyValue<K,V>> quKV = new Queue<KeyValue<K,V>>();

            public ChainEnumerator(ChainingHT<K,V> parent)
            {
                this.parent = parent;
            }

            public V Current => kvCurrent.Value;
            //public V Current => ((KeyValue<K,V>) ((ArrayList)parent.oDataArray[iCurrent])[iArrayIndex]).Value;

            //object IEnumerator.Current => ((ArrayList)parent.oDataArray[iCurrent]);

            object IEnumerator.Current => kvCurrent.Value;

            //object IEnumerator.Current => ((KeyValue<K, V>)((ArrayList)parent.oDataArray[iCurrent])[iArrayIndex]).Value;

            public void Dispose()
            {
                parent = null;
                //iArrayIndex = -1;
                iCurrent = -1;
            }

            public bool MoveNext()
            {
                bool bMoved = false;

                if (quKV.Count() > 0)
                {
                    kvCurrent = quKV.Dequeue();
                    bMoved = true;
                }
                else
                {
                    iCurrent++;
                    //iArrayIndex = -1;
                    while (!bMoved && iCurrent < parent.oDataArray.Length - 1)
                    {
                        if (parent.oDataArray[iCurrent] == null)
                        {
                            iCurrent++;
                        }
                        else
                        {
                            ArrayList al = (ArrayList)parent.oDataArray[iCurrent];

                            foreach (KeyValue<K,V> kv in al)
                            {
                                quKV.Enqueue(kv);
                            }

                            kvCurrent = quKV.Dequeue();
                            bMoved = true;
                        }
                    }
                }
                return bMoved;
            }

            public void Reset()
            {
                iCurrent = -1;
                //iArrayIndex = -1;
                quKV = new Queue<KeyValue<K, V>>();
                KeyValue<K, V> kv = null;
                kvCurrent = null;
            }

            bool FindNextArrayList()
            {
                bool bMoved = false;
                int iNextBucket = iCurrent;

                while(iNextBucket < parent.oDataArray.Length - 1 && !bMoved)
                {
                    iNextBucket++;
                    if(parent.oDataArray[iNextBucket] != null)
                    {
                        bMoved = true;
                        iCurrent = iNextBucket;
                    }
                }

                return bMoved;
            }
        }


        public override void Remove(K key)
        {
            int iHash = HashFunction(key);
            ArrayList alCurrent = (ArrayList)oDataArray[iHash];
            bool bFound = false;

            //If arraylist exists
            if(alCurrent != null)
            {
                KeyValue<K, V> kv = new KeyValue<K, V>(key, default(V));

                int iIndex = alCurrent.IndexOf(kv);

                if(iIndex >= 0)
                {
                    alCurrent.RemoveAt(iIndex);
                    bFound = true;

                    //Decrement hash table count
                    iCount--;

                    //Checks if the arraylist is empty, removes if it is
                    if(alCurrent.Count <= 0)
                    {
                        //Decrement bucket count, set current spot to null
                        oDataArray[iHash] = null;
                        iBucketCount--;
                    }
                }
            }

            //If array list is null or value wasn't found
            if(!bFound)
            {
                throw new ApplicationException("Value does not exist");
            }
        }

        //ToString - Hand Out Drive
        public override string ToString()
        {

            StringBuilder sb = new StringBuilder();
            //Loop through each bucket
            for (int i = 0; i < HTSize; i++)
            {
                //Add the bucket number to the string
                sb.Append("Bucket " + i.ToString() + ": ");
                //check if an ArrayList<KeyValue<K,V>> exists at this location
                if (oDataArray[i] != null)
                {
                    ArrayList alCurrent = (ArrayList)oDataArray[i];
                    foreach (KeyValue<K, V> kv in alCurrent)
                    {
                        sb.Append(kv.Value.ToString() + " --> ");
                    }
                    sb.Remove(sb.Length - 5, 5);
                }
                sb.Append("\n");

            }
            return sb.ToString();
        }
    }
}
