using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStructuresCommon;

namespace BinarySearchTree
{
    //Define a delegate type that will point to a method that will perform some action on a data member of type T
    public delegate void ProcessData<T>(T data);

    //An enumeration to determine order of iteration
    public enum TraversalOrder { PRE_ORDER, IN_ORDER, POST_ORDER };


    public interface I_BST<T> : I_Collection<T> where T: IComparable<T>
    {
        /// <summary>
        /// Given a data element, find the corresponding element of equal value.
        /// </summary>
        /// <param name="data">Data element to be found</param>
        /// <returns>Item of equal value</returns>
        T Find(T data);

        /// <summary>
        /// Returns the height of the tree
        /// </summary>
        /// <returns>The height of the three (# of hops between the root to the farthest down node)</returns>
        int Height();


        /// <summary>
        /// Similar to an enumerator, but more efficient. Also, the iterate method utilizes a delegate
        /// to perform some action on each data item
        /// </summary>
        /// <param name="pd">A delegate or function pointer to a method</param>
        /// <param name="to">The order in which the tree is traversed</param>
        void Iterate(ProcessData<T> pd, TraversalOrder to);


    }
}
