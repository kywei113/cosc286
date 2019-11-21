using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace Beeps
{
    class Program
    {
        static void Main(string[] args)
        {
            Music play = new Music(80);

            Thread playThread = new Thread(new ThreadStart(play.MinuetInG));

            playThread.Start();
            
        }
    }
}
