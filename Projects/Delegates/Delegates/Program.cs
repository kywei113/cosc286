using System;

namespace Delegates
{
    class Program
    {

        //Signature matches the delegate
        public static void DisplayInt(int x)
        {
            Console.WriteLine(x);
        }

        public static void DisplaySquare(int x)
        {
            Console.WriteLine(x * x);
        }

        public static void DisplayDouble(double x)
        {
            Console.WriteLine(x);
        }

        static void Main(string[] args)
        {
            Collection c = new Collection();

            c.iterate(DisplaySquare, Direction.BACKWARD);
            //c.iterate(DisplayInt);
            //c.iterate(DisplaySquare);
            //c.iterate(DisplayDouble);
            
        }
    }
}
