using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchTree
{
    public class Node<T> where T:IComparable<T>
    {
        #region Attributes
        private T tData;
        private Node<T> nLeft;
        private Node<T> nRight;
        #endregion

        #region Constructors

        public Node() : this(default(T), null, null) { }        //Remember, Pointers can point to null, T must have something. Use default(T)
        public Node(T tData) : this(tData, null, null) { }

        public Node(T tData, Node<T> nLeft, Node<T> nRight)
        {
            this.Data = tData;
            this.Left = nLeft;
            this.Right = nRight;
        }
        #endregion

        #region Properties
        //Note that the Get/Set are both included. Decide if you need both
        public T Data
        {
            get => tData;
            set => tData = value;
        }

        public Node<T> Left
        {
            get => nLeft;
            set => nLeft = value;
        }

        public Node<T> Right
        {
            get => nRight;
            set => nRight = value;
        }

        #endregion

        #region Other Functionality
        public bool IsLeaf()
        {
            return this.Left == null && this.Right == null;
        }
        #endregion
    }
}
