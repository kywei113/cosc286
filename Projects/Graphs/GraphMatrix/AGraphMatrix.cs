using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graphs;

namespace GraphMatrix
{
    public abstract class AGraphMatrix<T> : AGraph<T> where T : IComparable<T>
    {
        #region Attributes
        protected Edge<T>[,] matrix;

        #endregion

        #region Constructor
        public AGraphMatrix()
        {
            //Allocate the array object
            matrix = new Edge<T>[0,0];
        }
        #endregion

        //Function called from the Parent Edge
        protected override void AddEdge(Edge<T> e)
        {
            if(HasEdge(e.From.Data, e.To.Data))
            {
                throw new ApplicationException("Edge already exists");
            }
            //Index into the array and add the edge
            matrix[e.From.Index, e.To.Index] = e;

            //Increment the edge count
            numEdges++;
        }

        //Called from the parent class. Allocates space for the added vertex so it can store its edges
        public override void AddVertexAdjustEdges(Vertex<T> v)
        {
            //Create a reference to the old array
            Edge<T>[,] oldMatrix = matrix;

            //Create the new larger matrix
            matrix = new Edge<T>[NumVertices, NumVertices];

            //Copy edges from oldMatrix into new one
            for(int r = 0; r < oldMatrix.GetLength(0); r++)
            {
                for(int c = 0; c < oldMatrix.GetLength(1); c++)
                {
                    //Copy the current element to the new array
                    matrix[r, c] = oldMatrix[r, c];
                }
            }
        }

        public override void RemoveVertextAdjustEdges(Vertex<T> v)
        {
            //Reset numEdges since we are calling AddEdge to put the current edge into the new matrix
            numEdges = 0;

            //Create a reference to the old array
            Edge<T>[,] oldMatrix = matrix;

            //Create the new larger matrix
            matrix = new Edge<T>[NumVertices, NumVertices];

            //Copy edges from oldMatrix into new one
            for (int r = 0; r < oldMatrix.GetLength(0); r++)
            {
                for (int c = 0; c < oldMatrix.GetLength(1); c++)
                {
                    if(oldMatrix[r,c] != null)
                    {
                        if(r != v.Index && c != v.Index)
                        {
                            AddEdge(oldMatrix[r, c]);
                        }
                    }
                }
            }
        }

        public override IEnumerable<Vertex<T>> EnumerateNeighbours(T data)
        {
            List<Vertex<T>> neighbours = new List<Vertex<T>>();
            int row = GetVertex(data).Index;

            for(int c = 0; c < matrix.GetLength(1); c++)
            {
                if(matrix[row,c] != null)
                {
                    neighbours.Add(matrix[row, c].To);
                }
            }

            return neighbours;
        }

        public override Edge<T> GetEdge(T from, T to)
        {
            throw new NotImplementedException();
        }

        public override bool HasEdge(T from, T to)
        {
            return matrix[GetVertex(from).Index,GetVertex(to).Index] != null;
        }

        public override void RemoveEdge(T from, T to)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder("\nEdge Matrix:\n");

            for (int r = 0; r < matrix.GetLength(0); r++)
            {
                Vertex<T> v = vertices[r];
                result.Append(v.Data.ToString() + "\t");
                for (int c = 0; c < matrix.GetLength(1); c++)
                {
                    // result.Append((matrix[r, c] == null ? "null  " : matrix[r, c].To.ToString()) + "\t\t");
                    result.Append(String.Format("{0,12}", (matrix[r, c] == null ? "----  " : matrix[r, c].To.ToString())));
                }
                result.Append("\n");
            }

            //Return the vertices appended to the edges
            return base.ToString() + result;
        }
    }
}
