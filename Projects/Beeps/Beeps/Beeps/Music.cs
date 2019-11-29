using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.InteropServices;

namespace Beeps
{
    
    class Music
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool Beep(uint dwFreq, int dwDuration);

        #region Note Frequency Definitions
        private uint C3 = 131;
        private uint C3Sh = 139;
        private uint D3 = 147;
        private uint D3Sh = 156;
        private uint E3 = 165;
        private uint F3 = 175;
        private uint F3Sh = 185;
        private uint G3 = 196;
        private uint G3Sh = 208;
        private uint A3 = 220;
        private uint A3Sh = 233;
        private uint B3 = 247;

        private uint C4 = 262;
        private uint C4Sh = 277;
        private uint D4 = 294;
        private uint D4Sh = 311;
        private uint E4 = 330;
        private uint F4 = 350;
        private uint F4Sh = 370;
        private uint G4 = 392;
        private uint G4Sh = 415;
        private uint A4 = 440;
        private uint A4Sh = 466;
        private uint B4 = 494;

        private uint C5 = 523;
        private uint C5Sh = 554;
        private uint D5 = 587;
        private uint D5Sh = 622;
        private uint E5 = 659;
        private uint F5 = 698;
        private uint F5Sh = 740;
        private uint G5 = 784;
        private uint G5Sh = 831;
        private uint A5 = 880;
        private uint A5Sh = 932;
        private uint B5 = 988;

        private uint C6 = 1046;
        private uint C6Sh = 1109;
        private uint D6 = 1175;
        private uint D6Sh = 1245;
        private uint E6 = 1319;
        private uint F6 = 1397;
        private uint F6Sh = 1480;
        private uint G6 = 1568;
        private uint G6Sh = 1661;
        private uint A6 = 1760;
        private uint A6Sh = 1865;
        private uint B6 = 1976;

        private uint C7 = 2093;
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

        public void recalcBPM(int bpm)
        {
            beatDivisor = (double)bpm / bpm60;

            w = (int)(Math.Round((double)w / beatDivisor));
            h = (int)(Math.Round((double)h / beatDivisor));
            q = (int)(Math.Round((double)q / beatDivisor));
            e = (int)(Math.Round((double)e / beatDivisor));
            s = (int)(Math.Round((double)s / beatDivisor));
            ths = (int)(Math.Round((double)ths / beatDivisor));
        }


        #region Music Methods
        public void MiiChannel()
        {
            recalcBPM(114);

            //M1
            Beep(F4Sh, q);
            Beep(A4, e);
            Beep(C5Sh, e);
            Thread.Sleep(q);
            Beep(A4, e);
            Thread.Sleep(q);
            Beep(F4Sh, e);

            //M2
            Beep(D4, e);
            Beep(D4, e);
            Beep(D4, e);
            Thread.Sleep(h + e);
            Beep(C4Sh, e);

            //M3
            Beep(D4, e);
            Beep(F4Sh, e);
            Beep(A4, e);
            Beep(C5Sh, e);
            Thread.Sleep(q);
            Beep(A4, e);
            Thread.Sleep(q);
            Beep(F4Sh, e);

            //M4
            Beep(E5, q + e);
            Beep(D5Sh, e);
            Beep(D5, q);
            Thread.Sleep(q);

            //M5
            Beep(G4Sh, q);
            Beep(C5Sh, e);
            Beep(F4Sh, e);
            Thread.Sleep(q);
            Beep(C5Sh, e);
            Beep(G4Sh, e);

            //M6
            Thread.Sleep(q);
            Beep(C5Sh, e);
            Thread.Sleep(q);
            Beep(G4, e);
            Beep(F4Sh, e);
            Thread.Sleep(q);
            Beep(E4, e);
            Thread.Sleep(q);

            //M7
            Beep(E4, e);
            Beep(E4, e);
            Beep(E4, e);
            Thread.Sleep(e + q + e);
            Beep(E4, e);
            Beep(E4, e);
            Beep(E4, e);
        }

        private void Rest(int e)
        {
            Thread.Sleep(e);
        }
        
        

        #endregion



    }


}
