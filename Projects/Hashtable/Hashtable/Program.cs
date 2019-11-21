using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Hashtable
{
    class Program
    {
        static void TestAdd(A_Hashtable<int, string> ht)
        {
            try
            {
                ht.Add(1, "Key 1");
                ht.Add(11, "Key 11");
                ht.Add(111, "Key 111");
                ht.Add(47595, "bas");
            }
            catch(ApplicationException e)
            {
                Console.WriteLine(e.Message);
            }
           

        }

        static void LoadDataFromFile(A_Hashtable<Person, Person> ht)
        {
            StreamReader sr = new StreamReader(File.Open("People.txt", FileMode.Open));
            string sInput = "";

            try
            {
                //Read a line from the file
                while ((sInput = sr.ReadLine()) != null)
                {
                    try
                    {
                        char[] cArray = { ' ' };
                        string[] sArray = sInput.Split(cArray);
                        int iSSN = Int32.Parse(sArray[0]);
                        Person p = new Person(iSSN, sArray[2], sArray[1]);
                        ht.Add(p, p);

                    }
                    catch (ApplicationException ae)
                    {
                        Console.WriteLine("Exception: " + ae.Message);
                    }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            sr.Close();
        }

        static void LoadDataFromFileAndRemove(A_Hashtable<Person, Person> ht)
        {
            StreamReader sr = new StreamReader(File.Open("People.txt", FileMode.Open));
            string sInput = "";
            ArrayList al = new ArrayList();
            int iCount = 0;

            try
            {
                //Read a line from the file
                while ((sInput = sr.ReadLine()) != null)
                {
                    try
                    {
                        char[] cArray = { ' ' };
                        string[] sArray = sInput.Split(cArray);
                        int iSSN = Int32.Parse(sArray[0]);
                        Person p = new Person(iSSN, sArray[2], sArray[1]);
                        if (p.SSN == 478403546)
                        {
                            Console.WriteLine("");
                        };
                        ht.Add(p, p); 
                        if (iCount % 10 == 0)
                        {
                            al.Add(p);
                        }
                        iCount++;

                    }
                    catch (ApplicationException ae)
                    {
                        Console.WriteLine("Exception: " + ae.Message);
                    }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("Count before removing: " + ht.Count);
            //Remove every tenth person that is actually in the list.
            try
            {
                foreach (Person p in al)
                {
                    ht.Remove(p);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("Count after removing: " + ht.Count);
            sr.Close();
        }

        static void TestRemove(A_Hashtable<int, string> ht)
        {
            try
            {
                //ht.Remove(11);
                ht.Remove(1);
                ht.Remove(111);
            }
            catch (ApplicationException e)
            {
                Console.WriteLine(e.Message);
            }

            //ht.Remove(99999);
        }

        static void TestHT(A_Hashtable<Person, Person> ht)
        {
            LoadDataFromFileAndRemove(ht);
            Person testPerson = new Person(478403546, "Rebecca", "Mann");

            Console.WriteLine("Hash table type " + ht.GetType().ToString());
            
            TestEnumerator(ht);
            //Console.WriteLine(ht.ToString());
            Console.WriteLine("# of people: " + ht.Count);
            Console.WriteLine("Number of collisions: " + ht.NumCollisions);
            Console.WriteLine("Person Found: " + ht.Get(testPerson));
            Console.WriteLine("Person Found: " + ht.Get(testPerson));
            Console.WriteLine("Person Found: " + ht.Get(new Person(20765921, "", "")));
            Console.WriteLine("Person Found: " + ht.Get(new Person(725215354, "" ,"" )));
            Console.WriteLine("Person Found: " + ht.Get(new Person(111111111, "", "")));

            Console.WriteLine("\n\n");
        }

        static void TestEnumerator(A_Hashtable<Person, Person> ht)
        {
            int loops = 0;

            foreach(Person kv in ht)
            {
                Console.WriteLine(kv.ToString());
                loops++;
            }
            Console.WriteLine("Print loops executed: " + loops);
        }

        static void Main(string[] args)
        {
            //Modify to create ChainingHT for testing
            Linear<Person, Person> linear = new Linear<Person, Person>();
            //ChainingHT<Person, Person> chaining = new ChainingHT<Person, Person>();
            Quadratic<Person, Person> quadratic = new Quadratic<Person, Person>();

            DoubleHash<Person, Person> doubleHash = new DoubleHash<Person, Person>();



            //TestHT(linear);
            //TestHT(chaining);
            //TestHT(quadratic);
            TestHT(doubleHash);
        }
    }
}
