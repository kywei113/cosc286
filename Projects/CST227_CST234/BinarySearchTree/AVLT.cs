using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchTree
{
    public class AVLT<T>: BST<T> where T:IComparable<T>
    {

        internal override Node<T> Balance(Node<T> nCurrent)
        {
            Node<T> nNewRoot = nCurrent;

            if (nCurrent != null)
            {
                int iHeightDiff = GetHeightDifference(nCurrent);

                if (iHeightDiff < -1) //Right Heavy
                {
                    int iRightChildHeightDiff = GetHeightDifference(nCurrent.Right);
                    if (iRightChildHeightDiff > 0)
                    {
                        nNewRoot = DoubleLeft(nCurrent);
                    }
                    else
                    {
                        nNewRoot = SingleLeft(nCurrent);
                    }
                }
                else
                {
                    if (iHeightDiff > 1) //Left-Heavy
                    {
                        int iLeftChildHeightDiff = GetHeightDifference(nCurrent.Left);
                        if (iLeftChildHeightDiff < 0)
                        {
                            nNewRoot = DoubleRight(nCurrent);
                        }
                        else
                        {
                            nNewRoot = SingleRight(nCurrent);
                        }
                    }
                }
            }
            return nNewRoot;
        }

        #region Rotation Methods
        private Node<T> SingleLeft(Node<T> nOldRoot)
        {
            Node<T> nNewRoot = nOldRoot.Right;
            if(nNewRoot != null)
            {
                nOldRoot.Right = nNewRoot.Left;
                nNewRoot.Left = nOldRoot;
            }
            return nNewRoot != null ? nNewRoot : nOldRoot;
        }

        private Node<T> SingleRight(Node<T> nOldRoot)
        {

            Node<T> nNewRoot = nOldRoot.Left;
            if(nNewRoot != null)
            {
                nOldRoot.Left = nNewRoot.Right;
                nNewRoot.Right = nOldRoot;
            }
            return nNewRoot != null ? nNewRoot : nOldRoot;
        }

        private Node<T> DoubleLeft(Node<T> nOldRoot)
        {
            nOldRoot.Right = SingleRight(nOldRoot.Right);
            return SingleLeft(nOldRoot);

            //if(nOldRoot != null)
            //{
            //    nNewRoot = nOldRoot;
            //    nNewRoot.Right = SingleRight(nOldRoot.Right);
            //    nNewRoot = SingleLeft(nOldRoot);
            //}
            //return nNewRoot;
        }

        private Node<T> DoubleRight(Node<T> nOldRoot)
        {
            nOldRoot.Left = SingleLeft(nOldRoot.Left);
            return SingleRight(nOldRoot);

            //Node<T> nNewRoot = null;

            //if(nOldRoot != null)
            //{
            //    nNewRoot = nOldRoot;
            //    nNewRoot.Left = SingleLeft(nOldRoot.Left);
            //    nNewRoot = SingleRight(nOldRoot);
            //}
          
            //return nNewRoot;
        }

        public void TestLL()
        {
            nRoot = SingleLeft(nRoot);
        }
        public void TestRR()
        {
            nRoot = SingleRight(nRoot);
        }
        public void TestLR()
        {
            nRoot = DoubleLeft(nRoot);
        }
        public void TestRL()
        {
            nRoot = DoubleRight(nRoot);
        }
        #endregion



        #region Other Helper Methods
        /// <summary>
        /// Determines height different between left and right child nodes of the current node
        /// </summary>
        /// <param name="nCurrent"></param>
        /// <returns>Positive means left heavy, negative means right heavy. Absolute value is the height difference</returns>
        protected int GetHeightDifference(Node <T> nCurrent)
        {
            int iHeightLeft = -1;
            int iHeightRight = -1;
            int iHeightDiff = 0;

            if(nCurrent != null)
            {
                if(nCurrent.Right != null)
                {
                    iHeightRight = RecHeight(nCurrent.Right);
                }

                if (nCurrent.Left != null)
                {
                    iHeightLeft = RecHeight(nCurrent.Left);
                }

                //iHeightDiff = iHeightLeft > iHeightRight ? iHeightLeft - iHeightRight : iHeightRight - iHeightLeft;
                iHeightDiff = iHeightLeft - iHeightRight;
            }
            
            return iHeightDiff;
        }

        #endregion


    }
}
