using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public class Edge<T> : IComparable<Edge<T>> where T : IComparable<T>
    {
        #region Attributes
        private Vertex<T> from;
        private Vertex<T> to;
        private bool isWeighted;
        private double weight;
        #endregion


        #region Constructors

        /// <summary>
        /// If not weighted, we will store positive infinity for a weight
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public Edge(Vertex<T> from, Vertex<T> to)
            : this(from, to, double.PositiveInfinity, false)
        {
        }

        public Edge(Vertex<T> from, Vertex<T> to, double weight)
            : this(from, to, weight, true)
        {
        }

        /// <summary>
        /// This constructor is chained to the other constructors and never directly called by the user.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="weight"></param>
        /// <param name="isWeighted"></param>
        private Edge(Vertex<T> from, Vertex<T> to, double weight, bool isWeighted)
        {
            this.from = from;
            this.to = to;
            this.isWeighted = isWeighted;
            this.weight = weight;
        }
        #endregion

        #region Properties
        public Vertex<T> From { get => from; }
        public Vertex<T> To { get => to; }
        public bool IsWeighted { get => isWeighted; }
        public double Weight { get => weight; }

        #endregion


        public int CompareTo(Edge<T> other)
        {
            int result = 0;
            //Don't compare weights unless both edges have a weight
            if (other.isWeighted && this.isWeighted)
            {
                result = this.weight.CompareTo(other.weight);
            }
            //What if the edges have the same weight?
            if (result == 0)
            {
                //Compare the From vertices
                result = From.CompareTo(other.From);
                //If the from vertices are the same
                if (result == 0)
                {
                    result = To.CompareTo(other.To);
                }
            }
            return result;
        }

        public override bool Equals(object obj)
        {
            return this.CompareTo((Edge<T>)obj) == 0;
        }

        public override string ToString()
        {
            //Use the ternary operator to append the weight if it exists.
            return from + " To " + to + (isWeighted ? ", W = " + weight : "");
        }
    }
}
