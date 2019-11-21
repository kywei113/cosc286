using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchTree
{
    //Parallel.for(0,max,i);
    public class BST<T> : A_BST<T>, ICloneable where T : IComparable<T>
    {
        //Constructor - Not necessary, but increases readability
        public BST()
        {
            //Initialize the root node
            nRoot = null;

            //Set the count to 0
            iCount = 0;

        }

        #region Other Functionality
        public T FindSmallest()
        {
            if (nRoot != null)
            {
                return RecFindSmallest(nRoot);
            }
            else
            {
                throw new ApplicationException("Root is null");
            }
        }

        private T RecFindSmallest(Node<T> nCurrent)
        {
            return nCurrent.Left != null ? RecFindSmallest(nCurrent.Left) : nCurrent.Data;

            //Node<T> nSmall = new Node<T>(nCurrent.Data);

            //if(nCurrent.Left != null)
            //{
            //    nSmall = new Node<T>(RecFindSmallest(nCurrent.Left));
            //}

            //return nSmall.Data;
        }

        private T ImpRecFindSmallest(ref Node<T> nCurrent)
        {
            
            //return nCurrent.Left != null ? RecFindSmallest(nCurrent.Left) : nCurrent.Data;

            if (nCurrent.Left == null)
            {
                Node<T> nNode = new Node<T>();
                nNode.Right = nCurrent.Right;
                nNode.Left = nCurrent;
                nCurrent = nNode;
            }

            return nCurrent.Data;
        }

        public T FindLargest()
        {
            if (nRoot != null)
            {
                return RecFindLargest(nRoot);
            }
            else
            {
                throw new ApplicationException("Root is null");
            }
        }

        private T RecFindLargest(Node<T> nCurrent)
        {

            return nCurrent.Right != null ? RecFindLargest(nCurrent.Right) : nCurrent.Data;

            //Node<T> nLargest = new Node<T>(nCurrent.Data);

            //if(nCurrent.Right != null)
            //{
            //    nLargest = new Node<T>(RecFindLargest(nCurrent.Right));
            //}

            //return nLargest.Data;
        }




        #endregion
        #region ICloneable Implementation
        public object Clone()
        {
            if (this.nRoot != null)
            {
                BST<T> cloneTree = new BST<T>();
                cloneTree.nRoot = RecClone(this.nRoot);
                cloneTree.iCount = this.iCount;
                return cloneTree;
            }
            else
            {
                throw new ApplicationException("Root is null");
            }


        }

        private Node<T> RecClone(Node<T> nCurrent)
        {
            Node<T> nClone = new Node<T>(nCurrent.Data);

            if (nCurrent.Left != null)
            {
                nClone.Left = RecClone(nCurrent.Left);
            }

            if (nCurrent.Right != null)
            {
                nClone.Right = RecClone(nCurrent.Right);
            }

            return nClone;
        }
        #endregion


        #region A_BST Implementation
        public override T Find(T data)
        {
            T foundData = default(T);
            
            if(nRoot != null)
            {
                foundData = RecFind(nRoot, data);

                if (foundData.CompareTo(default(T)) == 0)
                {
                    throw new Exception(data.ToString() + " was not found within the tree");
                }
            }

            return foundData;

        }

        private T RecFind(Node<T> nCurrent, T data)
        {
            T foundData = default(T);
            
            if(nCurrent.Data.CompareTo(data) == 0)
            {
                foundData = nCurrent.Data;
            }
            else
            {
                if(nCurrent.Left != null)
                {
                    foundData = RecFind(nCurrent.Left, data);
                }

                if (nCurrent.Right != null)
                {
                    foundData = RecFind(nCurrent.Right, data);
                }
            }

            return foundData;
        }


        public override int Height()
        {
            int iHeight = -1;
            if(nRoot != null)
            {
                iHeight = RecHeight(nRoot);
            }

            return iHeight;
        }

        protected int RecHeight(Node<T> nCurrent)
        {
            int iHeightLeft = 0;
            int iHeightRight = 0;

            if(nCurrent.Left != null)
            {
                iHeightLeft = RecHeight(nCurrent.Left) + 1;
            }

            if(nCurrent.Right != null)
            {
                iHeightRight = RecHeight(nCurrent.Right) + 1;
            }

            return iHeightLeft >= iHeightRight ? iHeightLeft : iHeightRight;
        }



        public override void Iterate(ProcessData<T> pd, TraversalOrder to)
        {
            if (nRoot != null)
            {
                RecIterate(nRoot, pd, to);
            }
        }

        private void RecIterate(Node<T> current, ProcessData<T> pd, TraversalOrder to)
        {
            if(to == TraversalOrder.PRE_ORDER)
            {
                pd(current.Data);
            }
            


            if (current.Left != null)
            {
                RecIterate(current.Left, pd, to);
            }

            if (to == TraversalOrder.IN_ORDER)
            {
                pd(current.Data);
            }

            if (current.Right != null)
            {
                RecIterate(current.Right, pd, to);
            }

            if (to == TraversalOrder.POST_ORDER)
            {
                pd(current.Data);
            }
        }
        #endregion

        #region I_Collection Implementation
        /// <summary>
        /// If Root = Null
        ///     Create new node
        ///     Assign to root
        ///     Increment Count
        /// 
        /// If Current Node != Null
        ///     Check if incoming data is lower or higher than current node's data
        ///     If Lower than current
        ///        Check if left child exists
        ///        If left child = Null
        ///             Create new Node with incoming data
        ///             Assign new node to Current's left child
        ///        Else
        ///             Add to current's left's subtree (Recurse to the left)
        ///     If Higher than current
        ///         Check if right child exists
        ///         If right child = Null
        ///             Create new Node with incoming data
        ///             Assign new node to Current's right child
        ///        Else
        ///             Add to current's right's subtree (Recurse to the right)
        /// </summary>
        /// <param name="data">Data being added in</param>
        public override void Add(T data)
        {
            if(nRoot == null)
            {
                nRoot = new Node<T>(data);
            }
            else
            {
                RecAdd(data, nRoot);
                nRoot = Balance(nRoot);
            }
            iCount++;
        }

        //Virtual Keyword allows a child to override the method. CHILD DOES NOT HAVE TO OVERRIDE
        internal virtual Node<T> Balance(Node<T> nCurrent)
        {
            return nCurrent;
        }

        private void RecAdd(T data, Node<T> nCurrent)
        {
            //Compare the value passed in with the data stored in Current, storing result as iResult (CompareTo returns an int based on comparison result)
            int iResult = data.CompareTo(nCurrent.Data);
            
            if(iResult < 0) //If the data is less than nCurrent's data
            {
                //Check if left child exists
                if(nCurrent.Left == null)
                {
                    nCurrent.Left = new Node<T>(data);  //Build new node, assign as current node's left child
                }
                else
                {
                    RecAdd(data, nCurrent.Left);    //Recurse to the left
                    nCurrent.Left = Balance(nCurrent.Left);
                }
            }
            else //If the data is greater than nCurrent's data
            {
                //Check if right child exists
                if (nCurrent.Right == null)
                {
                    nCurrent.Right = new Node<T>(data); //Build new node, assign as current node's right child
                }
                else
                {
                    RecAdd(data, nCurrent.Right);   //Recurse to the right
                    nCurrent.Right = Balance(nCurrent.Right);
                }
            }
        }

        public override void Clear()
        {
            this.nRoot = null;
            this.iCount = 0;
        }

        public override IEnumerator<T> GetEnumerator()
        {
            return new BreadthFirstEnumerator(this);
        }
        /// <summary>
        /// Removes a specific data value within the BST. Replaces it with the largest value in the node's left subtree.
        /// Find node with data to be removed
        ///     Find largest in left subtree
        ///     Move largest value to node with removed value
        ///     Recursively remove the substitute value from the left subtree
        ///     
        ///     Find the smallest in the right subtree
        ///     Move smallest value to node with removed value
        ///     Recursively remove the substitute value from the right subtree
        ///     
        ///     Remove the final leaf node
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override bool Remove(T data)
        {
            bool bRemoved = false;

            nRoot = RecRemove(nRoot, data, ref bRemoved);
            nRoot = Balance(nRoot);

            return bRemoved;
        }

        /// <summary>
        ///     Substitute = default
        ///     if current is not null
        ///         compare = Compare data with current nodes data
        ///         if Data is less than currentData
        ///             Recursively remove value from current's left subtree
        ///         else
        ///             if Data is greater than currentData
        ///                 Recursively remove value from current's right subtree
        ///             else
        ///                 Remove the value
        ///                 wasRemoved = true
        ///                 if current is a leaf
        ///                     decrement treeCount
        ///                     set current to null
        ///                 else if its not a leaf
        ///                     if current's left subtree exists
        ///                         substitute = largest value in current's left subtree
        ///                         currentData = substitute
        ///                         currentLeft = Recursively Remove substitute from current's left subtree
        ///                     else current's right subtree must exist
        ///                         substitute = smallest value in current's right subtree
        ///                         currentData = substitute
        ///                         currentRight = Recursively remove substitute from current's right subtree
        ///                return current         
        /// </summary>
        /// <param name="nCurrent"></param>
        /// <param name="data"></param>
        /// <param name="bRemoved"></param>
        /// <returns></returns>
        private Node<T> RecRemove(Node<T> nCurrent, T data, ref bool bRemoved)
        {
            T subData = default(T);

            if (nCurrent != null)
            {
                int iCompare = data.CompareTo(nCurrent.Data);   //Compares data we're looking for against current data

                if (iCompare < 0)   //If data we're looking for is less than current, recurse left
                {
                    nCurrent.Left = RecRemove(nCurrent.Left, data, ref bRemoved);
                }
                else
                {
                    if (iCompare > 0)   //If data we're looking for is greater than current, recurse right
                    {
                        nCurrent.Right = RecRemove(nCurrent.Right, data, ref bRemoved);
                    }
                    else //if iCompare == 0, or data equals current data
                    {
                        nCurrent.Data = default(T);
                        bRemoved = true;

                        if (nCurrent.IsLeaf())  //If nCurrent is a leaf node
                        {
                            --this.iCount;      //Reduces tree's count
                            nCurrent = null;    //Sets nCurrent to null, removes the node
                        }
                        else
                        {
                            //Current node has two children, requires a substitute value
                            if(nCurrent.Left != null && nCurrent.Right != null)
                            {
                                subData = RecFindLargest(nCurrent.Left);    //Locates a substitute value from left tree
                                nCurrent.Data = subData;
                                nCurrent.Left = RecRemove(nCurrent.Left, subData, ref bRemoved);
                            }
                            //Else if the left child exists
                            else
                            {
                                if(nCurrent.Left != null)
                                {
                                    nCurrent = nCurrent.Left;
                                    this.iCount--;
                                }
                                //Only right child exists
                                else
                                {
                                    nCurrent = nCurrent.Right;
                                    this.iCount--;
                                }
                            }

                            //if (nCurrent.Left != null)
                            //{
                            //    subData = RecFindLargest(nCurrent.Left);
                            //    nCurrent.Data = subData;
                            //    nCurrent.Left = RecRemove(nCurrent.Left, subData, ref bRemoved);
                            //    nCurrent.Left = Balance(nCurrent.Left);
                                

                            //}
                            //else
                            //{
                            //    subData = RecFindSmallest(nCurrent.Right);
                            //    nCurrent.Data = subData;
                            //    nCurrent.Right = RecRemove(nCurrent.Right, subData, ref bRemoved);
                            //    nCurrent.Right = Balance(nCurrent.Right);
                            //}
                        }
                    }
                }
            }
            return nCurrent;
        }



        //Old Code
        //        private Node<T> RecRemove(Node<T> nCurrent, T data, ref bool bRemoved)
        //{
        //    T subData = default(T);

        //    if (nCurrent != null)
        //    {
        //        int iCompare = nCurrent.Data.CompareTo(data);

        //        if (iCompare > 0)
        //        {
        //            RecRemove(nCurrent.Left, data, ref bRemoved);
        //        }
        //        else
        //        {
        //            if (iCompare < 0)
        //            {
        //                RecRemove(nCurrent.Right, data, ref bRemoved);
        //            }
        //            else //if iCompare == 0, or current Data equals remove Data
        //            {
        //                nCurrent.Data = default(T);
        //                bRemoved = true;

        //                if (nCurrent.Left == null && nCurrent.Right == null)
        //                {
        //                    --this.iCount;
        //                    nCurrent = null;
        //                }
        //                else
        //                {
        //                    if (nCurrent.Left != null)
        //                    {
        //                        subData = RecFindLargest(nCurrent.Left);
        //                        nCurrent.Data = subData;
        //                        nCurrent.Left = RecRemove(nCurrent.Left, subData, ref bRemoved);
                                

        //                    }
        //                    else
        //                    {
        //                        subData = RecFindSmallest(nCurrent.Right);
        //                        nCurrent.Data = subData;
        //                        nCurrent.Right = RecRemove(nCurrent.Right, subData, ref bRemoved);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    return nCurrent;
        //}
        #endregion

        #region Enumerator Implementation
        private class BreadthFirstEnumerator : IEnumerator<T>
        {
            private BST<T> parent = null;
            private Node<T> nCurrent = null;
            private Queue<Node<T>> qNodes;

            public BreadthFirstEnumerator(BST<T> parent)
            {
                this.parent = parent;
                Reset();
            }

            public T Current => nCurrent.Data;

            object IEnumerator.Current => nCurrent.Data;

            public void Dispose()
            {
                parent = null;
                nCurrent = null;
                qNodes = null;
            }

            public bool MoveNext()
            {
                bool bMoved = false;

                if(qNodes.Count > 0)
                {
                    bMoved = true;
                    nCurrent = qNodes.Dequeue();

                    if(nCurrent.Left != null)
                    {
                        qNodes.Enqueue(nCurrent.Left);
                    }

                    if(nCurrent.Right != null)
                    {
                        qNodes.Enqueue(nCurrent.Right);
                    }
                }

                return bMoved;
            }

            public void Reset()
            {
                //Set up the enumerator to default state
                qNodes = new Queue<Node<T>>();

                //Place root node at start of the queue
                if (parent.nRoot != null)
                {
                    qNodes.Enqueue(parent.nRoot);
                }

                //Set current node to Null
                nCurrent = null;
            }
        }

        private class DepthFirstEnumerator : IEnumerator<T>
        {
            private BST<T> parent = null;
            private Node<T> nCurrent = null;
            private Stack<Node<T>> sNodes;

            public DepthFirstEnumerator(BST<T> parent)
            {
                this.parent = parent;
            }

            public T Current => nCurrent.Data;
            //Access the data from the current node
            object IEnumerator.Current => nCurrent.Data;

            public void Dispose()
            {
                parent = null;
                nCurrent = null;
                sNodes = null;
            }

            public bool MoveNext()
            {
                bool bMoved = false;

                //If there are still nodes left to process
                if(sNodes.Count > 0)
                {
                    bMoved = true;

                    //Gets the top item off of the stack
                    nCurrent = sNodes.Pop();

                    //Add the children to the stack
                    if(nCurrent.Right != null)
                    {
                        sNodes.Push(nCurrent.Right);
                    }

                    if(nCurrent.Left != null)
                    {
                        sNodes.Push(nCurrent.Left);
                    }

                }

                return bMoved;
            }

            public void Reset()
            {
                //Set up the enumerator to default state
                sNodes = new Stack<Node<T>>();

                //Push the root node on the stack
                if(parent.nRoot != null)
                {
                    sNodes.Push(parent.nRoot);
                }

                //Set current node to Null
                nCurrent = null;
            }
        }
        #endregion




    }
}
