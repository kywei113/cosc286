using System;
using System.Collections.Generic;
using System.Text;

namespace Delegates
{

    public enum Direction
    {
        FORWARD,
        BACKWARD
    }

    public class Collection
    {
        private int[] iArray = { 5, 10, 20, };

        //Creating an Enumeration


        //Create a delegate type
        public delegate void DoSomethingToData(int x);

        //Pass in an instance of the delegate
        public void iterate(DoSomethingToData ds, Direction dir)
        {
            //for (int i = 0; i < iArray.Length; i++)
            //{
            //    //console.writeline(iarray[i]);

            //    //calls the function using the delegate
            //    ds(iArray[i]);
            //}

            switch(dir)
            {
                case Direction.FORWARD:
                    for (int i = 0; i < iArray.Length; i++)
                    {
                        //console.writeline(iarray[i]);

                        //calls the function using the delegate
                        ds(iArray[i]);
                    }
                    break;

                case Direction.BACKWARD:
                    for (int i = iArray.Length - 1; i >= 0; i--)
                    {
                        //console.writeline(iarray[i]);

                        //calls the function using the delegate
                        ds(iArray[i]);
                    }
                    break;

                default:

                    break;
            }
        }
    }
}
