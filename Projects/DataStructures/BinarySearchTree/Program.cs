using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchTree
{
    class Program
    {
        static void DoSomethingToAnInt(int x)
        {
            Console.Write(x + " ");
        }


        static void TestIterate(BST<int> bst)
        {
            bst.Iterate(DoSomethingToAnInt, TraversalOrder.PRE_ORDER);
        }
        static void TestAdd(BST<int> bst)
        {
            bst.Add(10);
            bst.Add(5);
            bst.Add(15);
            bst.Add(1);
            bst.Add(8);
            bst.Add(11);
            bst.Add(43);
            bst.Add(20);
            bst.Add(9);
            bst.Add(7);
        }

        static void TestClear(BST<int> bst)
        {
            bst.Clear();
            Console.WriteLine("Tree Contents after Clear: " + bst.ToString());
            Console.WriteLine("Count: " + bst.Count);
        }

        static void TestFind(BST<int> bst)
        {
            try
            {
                Console.WriteLine("Finding 222: \t" + bst.Find(222));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void TestRemove(BST<int> bst)
        {
            bst.Remove(5);
            bst.Remove(8);
            bst.Remove(20);
            Console.WriteLine("Removing: \t" + bst.ToString());
        }

        static void TestRandomAVLT()
        {
            long start;
            long end;

            AVLT<int> balanceTree = new AVLT<int>();
            //BST<int> balanceTree = new BST<int>();

            start = Environment.TickCount;
            Random randomNumber = new Random((int)start);
            int iMax = 100000;
            int iLargest = iMax * 10;

            List<int> remove = new List<int>();

            int add;

            for(int i = 0; i < iMax; i++)
            {
                add = randomNumber.Next(1, iLargest);

                balanceTree.Add(add);

                if(i % 10 == 0)
                {
                    remove.Add(add);
                }
            }

            if(balanceTree.Count <= 50)
            {
                Console.WriteLine(balanceTree.ToString());
            }

            end = Environment.TickCount;

            Console.WriteLine("Time to Add: \t" + (end - start).ToString() + " ms");
            Console.WriteLine("Theoretical Minimum Height: \t " + Math.Truncate(Math.Log(iMax, 2)));
            Console.WriteLine("Actual Height: \t " + balanceTree.Height());

            foreach(int item in remove)
            {
                balanceTree.Remove(item);
            }

            if (balanceTree.Count <= 50)
            {
                Console.WriteLine(balanceTree.ToString());
            }
            //balanceTree.Iterate(DoSomethingToAnInt, TraversalOrder.IN_ORDER);
            Console.WriteLine("Count: \t" + balanceTree.Count);
            Console.WriteLine("Actual Height: \t " + balanceTree.Height());

        }

        static void TestHeightDiff()
        {
            AVLT<int> avlt = new AVLT<int>();
            avlt.Add(10);
            avlt.Add(15);
            avlt.Add(20);
        }

        static void TestRotations()
        {

            Console.WriteLine("Single Left");
            AVLT<int> avltLL = new AVLT<int>();
            avltLL.Add(10);
            avltLL.Add(15);
            avltLL.Add(13);
            avltLL.Add(20);
            //Console.WriteLine("Pre-Rotate:\t ");
            avltLL.Iterate(DoSomethingToAnInt, TraversalOrder.PRE_ORDER);
            //avltLL.TestLL();
            //Console.WriteLine("\nPost-Rotate: \t ");
            //avltLL.Iterate(DoSomethingToAnInt, TraversalOrder.PRE_ORDER);


            Console.WriteLine("\n\nSingle Right");
            AVLT<int> avltRR = new AVLT<int>();
            avltRR.Add(10);
            avltRR.Add(5);
            avltRR.Add(2);
            //Console.WriteLine("Pre-Rotate:\t ");
            avltRR.Iterate(DoSomethingToAnInt, TraversalOrder.PRE_ORDER);
            //avltRR.TestRR();
            //Console.WriteLine("\nPost-Rotate:\t ");
            //avltRR.Iterate(DoSomethingToAnInt, TraversalOrder.PRE_ORDER);



            Console.WriteLine("\n\nDouble Left");
            AVLT<int> avltLR = new AVLT<int>();
            avltLR.Add(5);
            avltLR.Add(10);
            avltLR.Add(8);
            //Console.WriteLine("Pre-Rotate:\t ");
            avltLR.Iterate(DoSomethingToAnInt, TraversalOrder.PRE_ORDER);
            //avltLR.TestLR();
            //Console.WriteLine("\nPost-Rotate:\t ");
            //avltLR.Iterate(DoSomethingToAnInt, TraversalOrder.PRE_ORDER);


            Console.WriteLine("\nDouble Right");
            AVLT<int> avltRL = new AVLT<int>();
            avltRL.Add(10);
            avltRL.Add(5);
            avltRL.Add(8);
            avltRL.Iterate(DoSomethingToAnInt, TraversalOrder.PRE_ORDER);
            //Console.WriteLine("\nPre-Rotate:");
            //avltRL.TestRL();
            //avltRL.Iterate(DoSomethingToAnInt, TraversalOrder.PRE_ORDER);
            //Console.WriteLine("Post-Rotate:");
        }
        static void Main(string[] args)
        {
            //BST<int> oakTree = new BST<int>();
            //TestAdd(oakTree);
            //TestIterate(oakTree);
            //Console.WriteLine(oakTree.ToString());
            //Console.WriteLine("Height: \t" + oakTree.Height());
            //Console.WriteLine("Smallest: \t" + oakTree.FindSmallest());
            //Console.WriteLine("Largest: \t" + oakTree.FindLargest());
            //Console.WriteLine(oakTree.Clone());
            //Console.WriteLine("Finding 15: \t" + oakTree.Find(15));

            //TestFind(oakTree);
            //TestRemove(oakTree);

            //TestClear(oakTree);

            TestRandomAVLT();
            //TestHeightDiff();
            //TestRotations();

        }
    }
}
