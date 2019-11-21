using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graphs;

namespace GraphMatrix
{
    public class UGraphMatrix<T> : AGraphMatrix<T> where T : IComparable<T>
    {
        public UGraphMatrix()
        {
            isDirected = false;
        }

        public override int NumEdges
        {
            get
            {
                //Note that base is used to call the parent version of a method
                return base.NumEdges / 2;
            }
        }

        //Since this is an undirected graph, adding an edge from A to B 
        //requires that we also add an edge from B to A. The user only adds
        //one of those edges
        public override void AddEdge(T from, T to)
        {
            //Adds an edge in both directions
            base.AddEdge(from, to);
            base.AddEdge(to, from);
        }

        public override void AddEdge(T from, T to, double weight)
        {
            //Adds an edge in both directions
            base.AddEdge(from, to, weight);
            base.AddEdge(to, from, weight);
        }

        protected override Edge<T>[] GetAllEdges()
        {
            List<Edge<T>> edges = new List<Edge<T>>();

            for (int r = 0; r < matrix.GetLength(0); r++)
            {
                for (int c = r + 1; c < matrix.GetLength(1); c++)
                {
                    //If the current location has an edge
                    if (matrix[r, c] != null)
                    {
                        edges.Add(matrix[r, c]);
                    }
                }
            }

            return edges.ToArray();
        }
    }
}
