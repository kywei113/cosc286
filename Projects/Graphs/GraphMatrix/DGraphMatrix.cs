using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graphs;

namespace GraphMatrix
{
    public class DGraphMatrix<T> : AGraphMatrix<T> where T : IComparable<T>
    {
        public DGraphMatrix()
        {
            isDirected = true;
        }

        protected override Edge<T>[] GetAllEdges()
        {
            List<Edge<T>> edges = new List<Edge<T>>();

            for (int r = 0; r < matrix.GetLength(0); r++)
            {
                for (int c = 0; c < matrix.GetLength(1); c++)
                {
                    //If the current location has an edge
                    if(matrix[r,c] != null)
                    {
                        edges.Add(matrix[r, c]);
                    }
                }
            }

            return edges.ToArray();
        }
    }
}
