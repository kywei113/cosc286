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
                array[i] = r.Next(array.Length * 100);  

                //Or
                /*
                 * array[i] = i;
                 * array[i] = arraySize - i
                 */
            }

            TestSorter(new InsertionSorter<int>(array));

        }

        static void Main(string[] args)
        {
            SorterTest();

        }
    }
}
