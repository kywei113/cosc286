using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Hashtable
{
    public abstract class A_OpenAddressing<K,V> : A_Hashtable<K,V>
        where K: IComparable<K>
        where V: IComparable<V>
    {
        //Abstract method to get the increment value (varies based on implementation)
        protected abstract int GetIncrement(int iAttempt, K key);

        //Create a class to generate our prime number sizes
        private PrimeNumber pn = new PrimeNumber();

        //Constructor to set up the hash table array
        public A_OpenAddressing()
        {
            oDataArray = new object[pn.GetNextPrime()];
        }


        #region A_Hashtable Implementations
        public override void Add(K key, V vValue)
        {
            //How many attempts were made to increment
            int iAttempt = 1;

            //Get the initial hash of the key passed in
            int iInitialHash = HashFunction(key);

            //The current location we are trying to add into
            int iCurrentLocation = iInitialHash;

            //Wrap the key and value in a KeyValue object
            KeyValue<K, V> kvNew = new KeyValue<K, V>(key, vValue);

            //Position to add to
            int iPositionToAdd = -1;

            //Find any empty location to add the current item
            while(oDataArray[iCurrentLocation] != null)
            {
                //If location contains a KeyValue
                if(oDataArray[iCurrentLocation].GetType() == typeof(KeyValue<K,V>))
                {
                    //Check the current key-value to make sure it's not the same as the key-value being added
                    KeyValue<K, V> kv = (KeyValue<K, V>)oDataArray[iCurrentLocation];
                    if(kv.Equals(kvNew))
                    {
                        throw new ApplicationException("Item already exists");
                    }
                }
                else //It is a tombstone
                {
                    //If it is the first tombstone in the collision chain, record its position
                    if (iPositionToAdd == -1)
                    {
                        iPositionToAdd = iCurrentLocation;
                    }
                }

                //Move to next location
                iCurrentLocation = iInitialHash + GetIncrement(iAttempt++, key);

                //Loop back to the top of the table if we go beyond the table
                iCurrentLocation %= HTSize;

                //for stats
                iNumCollisions++;

            }

            //If there are no tombstones
            if (iPositionToAdd == -1)
            {
                iPositionToAdd = iCurrentLocation;
            }

            //Add the key-value to the table
            oDataArray[iPositionToAdd] = kvNew;

            //Increment the count
            iCount++;

            if(IsOverloaded())
            {
                ExpandHashTable();
            }
        }

        private void ExpandHashTable()
        {
            //Create a reference to the existing hash table array
            object[] oOldArray = oDataArray;

            //Assign a new array to the hash table
            oDataArray = new Object[pn.GetNextPrime()];

            //Reset attributes
            this.iCount = 0;
            this.iNumCollisions = 0;

            //Add the items from the old array to the new array
            foreach (object ob in oOldArray)
            {
                if (ob != null && ob.GetType() != typeof(Tombstone))
                {
                    KeyValue<K, V> kvOB = (KeyValue<K, V>)ob;
                    this.Add(kvOB.Key, kvOB.Value);
                }
            }
        }

        private bool IsOverloaded()
        {
            return iCount / (double) HTSize > dLoadFactor;
        }

        /// <summary>
        /// Gets a KV object by hashing to the initial location of a key. If a collision occurs, increments to the next location.
        /// If a tombstone is found, the tombstone's location is stored. If the desired KV is found, it is moved to the first tombstone's
        /// location and removed from the location it was found.
        /// </summary>
        /// <param name="key">Key for the desired KV object</param>
        /// <returns></returns>
        public override V Get(K key)
        {
            int iAttempt = 1;   //Initial value for number of iterations/incremnets through a collision chain
            bool bFound = false;    //Whether the KV was found

            KeyValue<K, V> kvFind = new KeyValue<K, V>(key, default);

            int iInitialHash = HashFunction(key);   //Hashing the key to get the initial index
            int iCurrent = iInitialHash;    
            int iFirstTomb = -1;    //Holder for the index of the first tombstone. -1 by default

            while(!bFound)
            {
                if (oDataArray[iCurrent] != null && oDataArray[iCurrent].GetType() != typeof(Tombstone))    //If current spot is not null, and not a tombstone
                {
                    KeyValue<K, V> kv = (KeyValue<K,V>) oDataArray[iCurrent++]; //Creates a KV object from the current location's values
                    if (kv.Equals(kvFind))      //checks if the current location's object is the same as the one we're looking for
                    {
                        kvFind = kv;        //Assigns current location's object to kvFind
                        bFound = true;      //Sets found flag to true

                        if(iFirstTomb != -1)        //If a tombstone was found prior, value will not be -1
                        {
                            oDataArray[iFirstTomb] = kv;    //Sets KV object at index of first tombstone, overwriting the tombstone
                            oDataArray[iCurrent] = null;    //Removes KV object from current index
                        }
                    }
                }
                else
                {
                    if(iFirstTomb == -1 && oDataArray[iCurrent] != null)    //Checks if a prior tombstone was found and current location is not null
                    {
                        iFirstTomb = iCurrent;  //Saves tombstone index
                    }
                    iCurrent = (iInitialHash + GetIncrement(iAttempt++, key)) % HTSize; //Gets next increment to check
                }
            }
            return kvFind.Value;        //Returns value of found KV
        }

        public override void Remove(K key)
        {
            int iAttempt = 1;
            int iInitialHash = HashFunction(key);
            int iCurrentLocation = iInitialHash;
            bool bFound = false;

            KeyValue<K, V> kvKey = new KeyValue<K, V>(key, default(V));

            while (!bFound && oDataArray[iCurrentLocation] != null)
            {
                //If location contains a KeyValue
                if (oDataArray[iCurrentLocation].GetType() == typeof(KeyValue<K, V>))
                {
                    //Check the current key-value to make sure it's not the same as the key-value being added
                    KeyValue<K, V> kv = (KeyValue<K, V>)oDataArray[iCurrentLocation];

                    //if(kv.Key.CompareTo(key)==0)
                    if (kv.Equals(kvKey))
                    {
                        oDataArray[iCurrentLocation] = new Tombstone();
                        bFound = true;

                        //Decrement the count
                        iCount--;
                    }
                }
                iCurrentLocation = iInitialHash + GetIncrement(iAttempt++, key);

                //Loop back to the top of the table if we go beyond the table
                iCurrentLocation %= HTSize;
            }

            if(!bFound)
            {
                throw new ApplicationException(key + " was not found");
            }
        }

        public override IEnumerator<V> GetEnumerator()
        {
            return new OpenEnumerator(this);
        }

        private class OpenEnumerator : IEnumerator<V>
        {
            //Local reference to the hash table
            private A_OpenAddressing<K,V> parent = null;

            //Integer representing the current index
            private int iCurrent = -1;

            
            public OpenEnumerator(A_OpenAddressing<K,V> ht)
            {
                this.parent = ht;
            }

            public V Current => ((KeyValue<K, V>)parent.oDataArray[iCurrent]).Value;

            object IEnumerator.Current => ((KeyValue<K, V>)parent.oDataArray[iCurrent]).Value;

            public void Dispose()
            {
                parent = null;
                iCurrent = -1;
            }

            public bool MoveNext()
            {
                //Tracks if a value was found or not
                bool bMoved = false;
                //Moves to next location
                iCurrent++;

                //While we have not found a key-value and have not checked beyond the last position
                while(!bMoved && iCurrent < parent.HTSize)
                {
                    //If it is a null or a tombstone
                    if(parent.oDataArray[iCurrent] == null || parent.oDataArray[iCurrent].GetType() == typeof(Tombstone))
                    {
                        //Move to next location
                        iCurrent++;
                    }
                    else
                    {
                        //Value is a key/value, set found to true
                        bMoved = true;
                    }
                }
                return bMoved;
            }

            public void Reset()
            {
                iCurrent = -1;
            }
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < oDataArray.Length; i++)
            {
                sb.Append("Bucket " + i + ": ");
                if (oDataArray[i] != null)
                {
                    if (oDataArray[i].GetType() == typeof(KeyValue<K, V>))
                    {
                        KeyValue<K, V> kv = (KeyValue<K, V>)oDataArray[i];
                        sb.Append(kv.Value.ToString() + " IH = " + HashFunction(kv.Key));
                    }
                    else
                    {
                        sb.Append("Tombstone");
                    }
                }
                sb.Append("\n");
            }
            return sb.ToString();
        }
        #endregion
    }
}
