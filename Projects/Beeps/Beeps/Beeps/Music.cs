using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Beeps
{
    
    class Music
    {
        #region Note Frequency Definitions
        private int C3 = 131;
        private int C3Sh = 139;
        private int D3 = 147;
        private int D3Sh = 156;
        private int E3 = 165;
        private int F3 = 175;
        private int F3Sh = 185;
        private int G3 = 196;
        private int G3Sh = 208;
        private int A3 = 220;
        private int A3Sh = 233;
        private int B3 = 247;

        private int C4 = 262;
        private int C4Sh = 277;
        private int D4 = 294;
        private int D4Sh = 311;
        private int E4 = 330;
        private int F4 = 350;
        private int F4Sh = 370;
        private int G4 = 392;
        private int G4Sh = 415;
        private int A4 = 440;
        private int A4Sh = 466;
        private int B4 = 494;

        private int C5 = 523;
        private int C5Sh = 554;
        private int D5 = 587;
        private int E5 = 659;
        private int F5 = 698;
        private int F5Sh = 740;
        private int G5 = 784;
        private int G5Sh = 831;
        private int A5 = 880;
        private int A5Sh = 932;
        private int B5 = 988;

        private int C6 = 1046;
        private int C6Sh = 1109;
        private int D6 = 1175;
        private int D6Sh = 1245;
        private int E6 = 1319;
        private int F6 = 1397;
        private int F6Sh = 1480;
        private int G6 = 1568;
        private int G6Sh = 1661;
        private int A6 = 1760;
        private int A6Sh = 1865;
        private int B6 = 1976;

        private int C7 = 2093;
        #endregion

        #region Beats and Note Durations
        //Current object's divisor + base bpm60;
        private double beatDivisor = 1.0;
        private double bpm60 = 60.0;

        //Millisecond values @ 60 BPM
        private int w = 4000;       
        private int h = 2000;
        private int q = 1000;
        private int e = 500;
        private int s = 250;
        private int ths = 125;
        #endregion  

        //private Thread thr;

        //Default constructor for 60 BPM
        public Music()
        {
            //thr = th;
        }

        //Constructor for different BPMs, recalculates note durations based on divisor
        public Music(int bpm)
        {
            //thr = th;
            
            beatDivisor = (double)bpm / bpm60;

            w = (int)(Math.Round((double) w / beatDivisor));
            h = (int)(Math.Round((double)h / beatDivisor));
            q = (int)(Math.Round((double)q / beatDivisor));
            e = (int)(Math.Round((double)e / beatDivisor));
            s = (int)(Math.Round((double)s / beatDivisor));
            ths = (int)(Math.Round((double)ths / beatDivisor));
        }


        #region Music Methods
        public void MinuetInG()
        {
            //Line 1
            Task.Delay(q).Wait();
            Console.Beep(B4, e + s);
            Console.Beep(C5, s);

            Console.Beep(D5, e + s);
            Console.Beep(C5Sh, s);
            Console.Beep(D5, e + s);
            Console.Beep(C5Sh, s);
            Console.Beep(D5, e + s);
            Console.Beep(C5Sh, s);

            Console.Beep(D5, h);
            Console.Beep(E5, e + s);
            Console.Beep(B4, s);

            Console.Beep(C5, h);
            Console.Beep(D5, e + s);
            Console.Beep(A4, s);

            Console.Beep(B4, q);
            Task.Delay(q).Wait();

            Console.Beep(G4, e + s);
            Console.Beep(A4, s);

            Console.Beep(B4, e + s);
            Console.Beep(A4Sh, s);
            Console.Beep(B4, e + s);
            Console.Beep(A4Sh, s);
            Console.Beep(B4, e + s);
            Console.Beep(A4Sh, s);

            //Line 2
            Console.Beep(B4, h);

            Console.Beep(A4, e);
            Console.Beep(G4, e);

            Console.Beep(G4, e);
            Console.Beep(F4Sh, e);
            Console.Beep(F4Sh, e);
            Console.Beep(A4, e);
            Console.Beep(G4, e);
            Console.Beep(E4, e);

            Console.Beep(D4, q);
            Task.Delay(q).Wait();


            Console.Beep(D5, e);
            Console.Beep(G5, q);

            Console.Beep(G5, q);
            Console.Beep(F5Sh, q);
            Console.Beep(G5, q);

            Console.Beep(A5, h);
            Console.Beep(G5, s);
            Console.Beep(F5Sh, s);
            Console.Beep(E5, s);
            Console.Beep(D5, s);

            Console.Beep(C5, q);
            Console.Beep(B4, q);
            Console.Beep(E5, e + s);
            Console.Beep(C5, s);

            //Line 3
            Console.Beep(B4, q);
            Console.Beep(A4, e);
            Task.Delay(e).Wait();
            Console.Beep(G4, e + s);
            Console.Beep(A4, s);

            Console.Beep(B4, e + s);
            Console.Beep(A4Sh, s);
            Console.Beep(B4, e + s);
            Console.Beep(A4Sh, s);
            Console.Beep(B4, e + s);
            Console.Beep(A4Sh, s);

            Console.Beep(B4, h);
            Console.Beep(C5, e + s);
            Console.Beep(G4Sh, s);

            Console.Beep(A4, h);
            Console.Beep(B4, e + s);
            Console.Beep(F4Sh, s);

            Console.Beep(G4, q);
        }

        #endregion



    }


}
