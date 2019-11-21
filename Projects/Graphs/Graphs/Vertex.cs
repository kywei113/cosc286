using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public class Vertex<T> : IComparable<Vertex<T>> where T : IComparable<T>
    {
        #region Attributes
        public T data;     //The data the vertex stores
        public int index;  //Makes things more efficient when retrieving edges later on. Index of the vertex within the vertex array

        #endregion

        #region Constructors
        public Vertex(int index, T data)
        {
            this.index = index;
            this.data = data;
        }
        #endregion

        #region Properties
        public T Data { get => data; }
        public int Index { get => index; set => index = value; }

        #endregion

        public int CompareTo(Vertex<T> other)
        {
            return Data.CompareTo(other.Data);
        }

        public override string ToString()
        {
            return "[" + data + "(" + index + ")]";
        }

        public override bool Equals(object obj)
        {
            return this.CompareTo((Vertex<T>)obj) == 0;
        }


    }
}
