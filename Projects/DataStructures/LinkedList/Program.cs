using System;

namespace LinkedList
{
    class Program
    {
        public static void TestAdd(LinkedList<int> ll)
        {m
            ll.Add(3);
            ll.Add(5);
            ll.Add(7);
            ll.Add(10);
            ll.Add(15);
            ll.Add(11);

        }


        public static void TestInsert(LinkedList<int> ll)
        {
            try
            {
                ll.Insert(0, 99);
                ll.Insert(3, 40);
                ll.Insert(8, 50);
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public static void TestRemove(LinkedList<int> ll)
        {
            try
            {
                Console.WriteLine(ll.Remove(99));
                Console.WriteLine(ll.Remove(40));
                Console.WriteLine(ll.Remove(50));
                Console.WriteLine(ll.Remove(10000));
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public static void TestRemoveAt(LinkedList<int> ll)
        {
            ll.RemoveAt(5);


        }
        public static void TestClear(LinkedList<int> ll)
        {
            ll.Clear();
        }

        public static void TestReplaceat(LinkedList<int> ll)
        {
            ll.ReplaceAt(4, 500);
        }

        public static void TestIndexOf(LinkedList<int> ll)
        {
            Console.WriteLine(ll.IndexOf(0));
            Console.WriteLine(ll.IndexOf(3));
            Console.WriteLine(ll.IndexOf(5));
            Console.WriteLine(ll.IndexOf(7));
            Console.WriteLine(ll.IndexOf(10));
            Console.WriteLine(ll.IndexOf(500));
            Console.WriteLine(ll.IndexOf(7));
        }
        static void Main(string[] args)
        {
            LinkedList<int> ll = new LinkedList<int>();

            TestAdd(ll);
            TestInsert(ll);

            Console.WriteLine(ll.ToString());
            TestRemove(ll);
            Console.WriteLine(ll.ToString());

            TestRemoveAt(ll);
            Console.WriteLine(ll.ToString());

            TestReplaceat(ll);
            Console.WriteLine(ll.ToString());

            TestIndexOf(ll);
            //TestClear(ll);
            //Console.WriteLine(ll.ToString());
        }
    }
}
