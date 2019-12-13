using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting
{
    class Program
    {
        public static void TestSorter(ASorter<int> Sorter)
        {
            //Display the sorter type
            Console.WriteLine(Sorter.GetType().Name + " with " + Sorter.Length + " elements");

            //Display the array if it is shorter than 50 elements
            if(Sorter.Length <= 50)
            {
                Console.WriteLine("Before Sort: \n" + Sorter);
            }

            //Calculate the approximate time taken
            long startTime = Environment.TickCount;
            Sorter.Sort();
            long endTime = Environment.TickCount;
            long finalTime = endTime - startTime;

            if (Sorter.Length <= 50)
            {
                Console.WriteLine("After Sort: \n" + Sorter);
            }
            //Display time elapsed to sort

            Console.WriteLine("Time taken to sort: " + finalTime + " milliseconds");
        }

        public static void SorterTest()
        {
            Console.WriteLine("Enter number of elements: ");
            String input = Console.ReadLine();
            int arraySize = Int32.Parse(input);
            int[] array = new int[arraySize];

            //Seed the random number generator to get a different sequence every time we run this
            Random r = new Random(Environment.TickCount);

            for(int i = 0; i < arraySize; i++)
            {
                //array[i] = r.Next(array.Length);

                //Or
                //Best case scenario for Insertion Sort - Values already sorted In Order
                //array[i] = i;

                //Worst case scenario for insertion sort - Values sorted in opposite order
                array[i] = arraySize - i;


            }

            //TestSorter(new InsertionSorter<int>(array));
            //TestSorter(new QuickSorterMedianThree<int>(array));
            TestSorter(new HeapSorter<int>(array));

        }

        private static void DemoStableSort()
        {
            StableDemo[] array = new StableDemo[10];
            Random r = new Random(42);

            for(int i = 0; i < array.Length; i++)
            {
                array[i] = new StableDemo(r.Next(array.Length), i);
            }

            //Creating stable sorter
            //ASorter<StableDemo> sorter = new InsertionSorter<StableDemo>(array);

            //Unstable
            //ASorter<StableDemo> sorter = new QuickSorter<StableDemo>(array);

            //Unstable
            ASorter<StableDemo> sorter = new HeapSorter<StableDemo>(array);

            Console.WriteLine("Before Sort:\n" + sorter);
            sorter.Sort();
            Console.WriteLine("After Sort:\n" + sorter);
        }


        static void Main(string[] args)
        {
            //SorterTest();
            DemoStableSort();

        }
    }
}
