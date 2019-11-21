using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public delegate void VisitorDelegate<T>(T data);

    public interface IGraph<T> where T : IComparable<T>
    {
        #region Properties
        int NumVertices { get; }
        int NumEdges { get; }
        #endregion

        #region Methods to work with Vertices
        void AddVertex(T data);

        void RemoveVertex(T data);

        bool HasVertex(T data);

        Vertex<T> GetVertex(T data);

        IEnumerable<Vertex<T>> EnumerateVertices();

        IEnumerable<Vertex<T>> EnumerateNeighbours(T data);
        #endregion

        #region Metohds to work with Edges
        void AddEdge(T from, T to);

        void AddEdge(T from, T to, double weight);

        bool HasEdge(T from, T to);

        Edge<T> GetEdge(T from, T to);

        void RemoveEdge(T from, T to);
        #endregion

        #region Definition of Essential Graph Algorithms
        IGraph<T> ShortestWeightedPath(T start, T end);

        IGraph<T> MinimumSpanningTree();

        void DepthFirstTraversal(T start, VisitorDelegate<T> whatToDo);

        void BreadthFirstTraversal(T start, VisitorDelegate<T> whatToDo);

        #endregion
    }
}
