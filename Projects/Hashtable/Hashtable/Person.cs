﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hashtable
{
    public class Person: IComparable<Person>
    {

        int ssn = 0;
        string sFirstName = "";
        string sLastName = "";
        //Not part of the data, but used for collision count
        int iCount = 1;

        public Person(int ssn, string sFirstName, string sLastName)
        {
            this.ssn = ssn;
            this.sFirstName = sFirstName;
            this.sLastName = sLastName;
        }

        public int SSN
        {
            get { return this.ssn; }
        }

        public string FirstName
        {
            get { return this.sFirstName; }
        }

        public string LastName
        {
            get { return this.sLastName; }
        } 

        public int Count
        {
            get { return iCount; }
            set { iCount = value; }
        }

        /// <summary>
        /// Only override this method if you know what you are doing
        /// A good hash function will generate values that represent all indicies of the hash table array, not just a subset.
        /// Each index should have an equal chance of being generated.
        /// A hash function is dependent on the key as well as the hash table size.
        /// A hash function may work well with some keys but not others.
        /// A hash function may work well on smaller tables but not well with larger tables
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            char[] cChars = ssn.ToString().ToCharArray();
            int iHashCode = 0;
            const int iPrime = 31; //Value Java uses
            for (int i = 0; i < cChars.Length; i++)
            {
                /*This is an example of a poor hash code.  Each SSN number is 8 or 9 ascii digits
                 *Each ascii digit has a value of 48 to 57.  The sum of the digits will therefore range 
                 *from a low of 8*48 = 384 to a high of 9*57 = 513.  This means that the maximum number
                 *unique hash locations is 513-384+1 = 130, and will likely be much less.
                 */
                //iHashCode += (int)cChars[i];

                /*This modification multiplies each ascii value by a power of 9 according to its position
                 *in the ssn.  For this set of data, a much better distribution occurs.
                 */
                //iHashCode += ((int)cChars[i]) * (int)Math.Pow(9, i);

                //Commonly used algorithm involving a prime number
                iHashCode = (iPrime * iHashCode + cChars[i]);
            }

            return iHashCode;
            //return ssn.GetHashCode();
        }
        public override string ToString()
        {
            return ssn.ToString() + " " + LastName + ", " + FirstName;
        }

        public override bool Equals(object obj)
        {
            Person pTemp = (Person)obj;
            return this.Equals(pTemp.ssn);
        }

        public int CompareTo(Person other)
        {
            return this.ssn.CompareTo(other.ssn);
        }
    }
}
