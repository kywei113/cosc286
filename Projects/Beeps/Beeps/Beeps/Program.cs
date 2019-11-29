using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.InteropServices;


namespace Beeps
{
    class Program
    {
        static void Main(string[] args)
        {
            Music play = new Music(60);

            //ThreadStart playTS = new ThreadStart(play.MiiChannel);
            Thread playThread = new Thread(new ThreadStart(play.MiiChannel));

            playThread.Start();

            //Thread test = new Thread(new ThreadStart(Numbers));
            //test.Start();   
        }

        public static void Numbers()
        {
            Console.WriteLine(1);
            Thread.Sleep(1000);
            Console.WriteLine(2);
            Thread.Sleep(1000);
            Console.WriteLine(3);
            Thread.Sleep(1000);
            Console.WriteLine(4);
            Thread.Sleep(1000);
            Console.WriteLine(5);
            Rest(1000);
            Console.WriteLine(6);
            Rest(1000);
            Console.WriteLine(7);
            Rest(1000);
            Console.WriteLine(8);
            Rest(1000);
            Console.WriteLine(9);
            Rest(1000);
            Console.WriteLine(10);
        }

        private static void Rest(int rest)
        {
            Thread.Sleep(rest);
        }
    }

    //class Program
    //{
    //    [DllImport("kernel32.dll", SetLastError = true)]
    //    static extern bool Beep(uint dwFreq, uint dwDuration);

    //    static void Main()
    //    {
    //        Console.WriteLine("Testing PC speaker...");
    //        for (uint i = 100; i <= 20000; i++)
    //        {
    //            Beep(i, 100);
    //        }
    //        Console.WriteLine("Testing complete.");

    //    }
    //}
}
