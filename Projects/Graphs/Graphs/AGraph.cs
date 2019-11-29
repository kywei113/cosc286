using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public abstract class AGraph<T> : IGraph<T>
        where T : IComparable<T>
    {
        #region Attributes
        //Stores the vertices of the graph within a list
        protected List<Vertex<T>> vertices;

        //A dictonary is essentially a hashtable, but it supports generics.
        //Will use it to store a data item's index into a vertice list.
        //Makes vertex lookups from the vertice list more efficient
        protected Dictionary<T, int> revLookUp;

        //Stores number of edges in the graph
        protected int numEdges;

        //Is the graph directed or not
        protected bool isDirected;

        //Is the graph weighted or not
        protected bool isWeighted;
        #endregion

        #region Constructor
        public AGraph()
        {
            vertices = new List<Vertex<T>>();
            revLookUp = new Dictionary<T, int>();
            numEdges = 0;
        }
        #endregion

        #region IGraph Implementations
        public int NumVertices
        {
            get
            {
                return vertices.Count;
            }
        }

        public virtual int NumEdges
        {
            get
            {
                return numEdges;
            }
        }

        public virtual void AddEdge(T from, T to)
        {
            //If this is the first edge, then the graph will be unweighted.
            if(numEdges == 0)
            {
                isWeighted = false;
            }
            else
            {
                //If the graph is already weighted
                if(isWeighted)
                {
                    throw new ApplicationException("Can't add an unweighted edge to a weighted graph");
                }
            }

            //Create an edge object
            
            Edge<T> e = new Edge<T>(GetVertex(from), GetVertex(to));
            AddEdge(e);
        }

        public virtual void AddEdge(T from, T to, double weight)
        {
            //If this is the first edge, then the graph will be weighted
            if(numEdges == 0)
            {
                isWeighted = true;
            }
            else
            {
                if(!isWeighted) //If the graph is already non-weighted
                {
                    throw new ApplicationException("Can't add a weighted edge to an unweighted graph");
                }
            }

            //Create the edge object
            Edge<T> e = new Edge<T>(GetVertex(from), GetVertex(to), weight);

            //Add the edge object to the graph
            AddEdge(e);
        }

        public void AddVertex(T data)
        {
            //If this vertex already exists, throw an exception
            if (HasVertex(data))
            {
                throw new ApplicationException("Vertex already exists");
            }
            else
            {
                //Instantiate a vertex containing data
                Vertex<T> v = new Vertex<T>(vertices.Count, data);

                //Add the vertex to the list
                vertices.Add(v);

                //Also store the index in the dictionary
                revLookUp.Add(data, v.Index);

                //Tell the child class to add room for the edges
                AddVertexAdjustEdges(v);
            }
        }

        //Note that the vertices collection is of type IEnumerable, so we can just return it without additional changes
        public IEnumerable<Vertex<T>> EnumerateVertices()
        {
            return vertices;
        }



        public Vertex<T> GetVertex(T data)
        {
            if(!HasVertex(data))
            {
                throw new ApplicationException("Vertex does not exist. (Error: -0x0012FF7C)");  //The error code adds $10,000 to your paycheck
            }
            else
            {
                //Reverse lookup the index in the dictionary
                //Note: C# overloads the [] operator for dictionaries
                int index = revLookUp[data];

                return vertices[index];
            }
        }

        public bool HasVertex(T data)
        {
            //Most efficient way is to look up in the dictionary
            return revLookUp.ContainsKey(data);
        }

        public void RemoveVertex(T data)
        {
            if(HasVertex(data))
            {
                Vertex<T> v = GetVertex(data);

                //Remove the vertex from the vertices collection
                vertices.Remove(v);
                
                //Remove from the dictionary
                revLookUp.Remove(data);

                //For all vertices below the vertex removed
                for(int i = v.Index; i < vertices.Count; i++)
                {
                    //Update the current vertex object's index
                    vertices[i].Index--;

                    //Update the index stored in the dictionary
                    revLookUp[vertices[i].Data]--;
                }

                //Tell the child class to remove edges associated with this vertex
                RemoveVertextAdjustEdges(v);
            }
            else
            {
                throw new ApplicationException("Vertex does not exist");
            }
        }
        #endregion

        #region Fundamental Graph Algorithms
        public void BreadthFirstTraversal(T start, VisitorDelegate<T> whatToDo)
        {
            //Get the starting vertex
            Vertex<T> startVertex = GetVertex(start);

            //Reference to the current vertex
            Vertex<T> currentVertex = null;

            //Dictionary to track the vertices already visited <T,T>
            Dictionary<T, T> visited = new Dictionary<T, T>();
            //Stack to store the current nodes neighbours
            Queue<Vertex<T>> quVertex = new Queue<Vertex<T>>();

            //Push the starting vertex onto the stack
            quVertex.Enqueue(startVertex);

            //while there are vertices on the stack
            while (quVertex.Count > 0)
            {

                currentVertex = quVertex.Dequeue();                             //Current vertex <-- pop top item off of the stack
                if (!visited.ContainsKey(currentVertex.Data))                //If the current vertex has NOT been visited
                {
                    whatToDo(currentVertex.Data);                    //Process the current vertex
                    visited.Add(currentVertex.Data, currentVertex.Data);                    //Mark as visited
                    IEnumerable<Vertex<T>> neighbours = EnumerateNeighbours(currentVertex.Data);                    //Get a list of neighbours for current vertex (helper function)

                    foreach (Vertex<T> vt in neighbours)                    //For each vertex in the list
                    {
                        quVertex.Enqueue(vt);                        //Push vertex onto stack
                    }
                }
            }
        }

        public void DepthFirstTraversal(T start, VisitorDelegate<T> whatToDo)
        {
            //Get the starting vertex
            Vertex<T> startVertex = GetVertex(start);

            //Reference to the current vertex
            Vertex<T> currentVertex = null;

            //Dictionary to track the vertices already visited <T,T>
            Dictionary<T, T> visited = new Dictionary<T, T>(NumVertices);

            //Stack to store the current nodes neighbours
            Stack<Vertex<T>> stVertex = new Stack<Vertex<T>>();

            //Push the starting vertex onto the stack
            stVertex.Push(startVertex);

            //while there are vertices on the stack
            while (stVertex.Count > 0)
            {
                currentVertex = stVertex.Pop();                             //Current vertex <-- pop top item off of the stack
                if (!visited.ContainsKey(currentVertex.Data))                //If the current vertex has NOT been visited
                {
                    whatToDo(currentVertex.Data);                    //Process the current vertex
                    visited.Add(currentVertex.Data, currentVertex.Data);                    //Mark as visited
                    IEnumerable<Vertex<T>> neighbours = EnumerateNeighbours(currentVertex.Data);   //Get a list of neighbours for current vertex (helper function)

                    foreach (Vertex<T> vt in neighbours)                    //For each vertex in the list
                    {
                        stVertex.Push(vt);                        //Push vertex onto stack
                    }

                }
            }
        }

        /// <summary>
        /// Given the forest (array of graphs), remove the location indicated by treeCut
        /// </summary>
        /// <param name="treeCut">Index of item to remove</param>
        /// <param name="forest">Array of graphs</param>
        /// <returns></returns>
        private AGraph<T> [] Timber(int treeCut, AGraph<T>[] forest)
        {
            AGraph<T>[] tempForest = new AGraph<T>[forest.Length - 1];
            int j = 0;  //Loop counter for the new forest
            for(int i = 0; i < forest.Length; i++)
            {
                if(i != treeCut)
                {
                    tempForest[j++] = forest[i];
                }
            }

            return tempForest;
        }

        /// <summary>
        /// Merge treeFrom with treeTo (this)
        /// Copy all vertices over
        /// Copy all edges over
        /// </summary>
        /// <param name="treeFrom">tree we are merging in</param>
        private void MergeTrees(AGraph<T> treeFrom)
        {
            foreach(Vertex<T> v in treeFrom.EnumerateVertices())
            {
                this.AddVertex(v.Data);
                //this.AddVertexAdjustEdges(v);
            }

            foreach (Edge<T> e in treeFrom.GetAllEdges())
            {
                this.AddEdge(e.From.data, e.To.data, e.Weight);
            }
        }

        /// <summary>
        /// Given a forest and a vertex, find the index of the given vertex in the forest
        /// </summary>
        /// <param name="to"></param>
        /// <param name="forest"></param>
        /// <returns></returns>
        private int FindTree(Vertex<T> to, AGraph<T>[] forest)
        {
            int i = 0;
            //Check each tree in the forest for the vertex
            while (!forest[i].HasVertex(to.Data) && ++i < forest.Length);

            //If the node is not found
            if(i == forest.Length)
            {
                throw new ApplicationException("Node not found in forest");
            }
            return i;
        }

        public IGraph<T> MinimumSpanningTree()
        {
            //Need to create an instance of the child class within the parent class. Don't know which child to create
            //Possible Solutions
            //1 - Create an abstract method where the child can return an instance of itself

            //2 - Use C# Reflection to return an instance of the child utilizing the GetType()
            AGraph<T> aGraph = null;
            List<AGraph<T>> forest = new List<AGraph<T>>();
            EdgeComparer comparer = new EdgeComparer();
            List<Edge<T>> liEdges = new List<Edge<T>>();

            Edge<T>[] edges = this.GetAllEdges();
            foreach(Edge<T> e in edges)
            {
                liEdges.Add(e);
            }

            liEdges.Sort(comparer);

            foreach(Vertex<T> v in vertices)
            {
                aGraph = (AGraph<T>)GetType().Assembly.CreateInstance(this.GetType().FullName);
                aGraph.AddVertex(v.Data);
                forest.Add(aGraph);
            }

            //while(liEdges.Count > 0 && forest.Count > 1)
            //{
            foreach(Edge<T> e in liEdges)
            {
                int fromIndex = forest.FindIndex(g => g.vertices.Contains(e.From));
                int toIndex = forest.FindIndex(g => g.vertices.Contains(e.To));

                if (fromIndex != toIndex && fromIndex != -1 && toIndex != -1)
                {
                    forest[fromIndex].MergeTrees(forest[toIndex]);
                    forest[fromIndex].AddEdge(e.From.Data, e.To.Data, e.Weight);
                    forest.RemoveAt(toIndex);
                }
            }
            //}

            if(forest.Count == 1)
            {
                aGraph = forest[0];
            }

            return aGraph;
        }

        /// <summary>
        /// This inner class is used by Array.Sort() method to sort our array of edges.
        /// Specifically, this class will compare two edge objects.
        /// </summary>
        private class EdgeComparer : IComparer<Edge<T>>
        {
            public int Compare(Edge<T> x, Edge<T> y)
            {
                return x.CompareTo(y);
            }
        }




        public IGraph<T> ShortestWeightedPath(T start, T end)
        {
            /*
             vTable <-- new array of VertexData objects
             startingIndex <-- index of starting points

            Load vTable with initial data
            Set start vertex's distance to 0
            Create priority queue and enqueue the starting vertexData item

            while there are still vertices in the priority queue
                currentVertex <-- priorityQueue dequeue
                if the currentVertex is not known
                    set currentVertex to known

                    for each neighbour wVertex of current
                        wVertexData <-- get the VertexData object of wVertex
                        edge <-- get the edge object connecting current to wVertex (need its weight)
                        proposedDistance <-- current's distance + cost of edge

                        if wVertexData's distance > proposedDistance
                            wVertexData's distance <-- proposedDistance
                            wVertexData's previous <-- currentVertex
                            PriorityQueue enqueue wVertexData

            Return a graph indicating the shortest path from start to end
             */

            List<VertexData> vTable = new List<VertexData>();

            foreach(Vertex<T> v in vertices)
            {
                VertexData vd = new VertexData(v, double.PositiveInfinity, null);
                vTable.Add(vd);
            }

            int startingIndex = GetVertex(start).Index;
            vTable[startingIndex].Distance = 0;
            PriorityQueue pQueue = new PriorityQueue();
            pQueue.Enqueue(vTable[startingIndex]);

            while(!pQueue.IsEmpty())
            {
                VertexData currentVertexData = pQueue.Dequeue();
                
                if(!currentVertexData.Known)
                {
                    currentVertexData.Known = true;

                    foreach(Vertex<T> neighbourV in EnumerateNeighbours(currentVertexData.Vertex.Data))
                    {
                        VertexData vd = vTable[neighbourV.Index];
                        Edge<T> e = GetEdge(currentVertexData.Vertex.Data, neighbourV.Data);

                        double propCost = currentVertexData.Distance + e.Weight;

                        if(vd.Distance > propCost)
                        {
                            vd.Distance = propCost;
                            vd.Previous = currentVertexData.Vertex;
                            pQueue.Enqueue(vd);
                        }
                    }
                }
            }

            return BuildGraph(GetVertex(end), vTable.ToArray());
        }

        internal class VertexData : IComparable
        {

            public Vertex<T> Vertex;            //The vertex
            public double Distance;             //Tentative distance form this vertex back to the start
            public bool Known;                  //Whether the distnacne is tentative or the actual shortest distance
            public Vertex<T> Previous;          //The prveious vertex for the current vertex

            public VertexData(Vertex<T> vertex, double distance, Vertex<T> previous, bool known = false)
            {
                this.Vertex = vertex;
                this.Distance = distance;
                this.Previous = previous;
                this.Known = known;
            }

            public int CompareTo(object obj)
            {
                return this.Distance.CompareTo(((VertexData)obj).Distance);
            }

            public override string ToString()
            {
                return "Vertex: " + Vertex + " Distance: " + Distance + " Previous: " + Previous.Data + " Known: " + Known;
            }
        }

        private IGraph<T> BuildGraph(Vertex<T> vEnd, VertexData[] vTable)
        {
            /*
             result <-- Instantiate a new graph instance
             Add the End Vertex to the result
             dataLast <-- vTable[Location of End]
             previous <-- Previous of dataLast

            while previous is not null
                Add previous to result
                Add the edge from last and previous

                dataLast <--vTable[Location of previous]
                previous <--previous of dataLast

            return result
             */

            IGraph<T> result = (IGraph<T>)GetType().Assembly.CreateInstance(this.GetType().FullName);

            result.AddVertex(vEnd.Data);

            VertexData dataLast = vTable[vEnd.Index];
            Vertex<T> prev = dataLast.Previous;

            while (prev != null)
            {
                result.AddVertex(prev.Data);

                Edge<T> eEdge = GetEdge(prev.Data, dataLast.Vertex.Data);

                result.AddEdge(eEdge.From.Data, eEdge.To.Data, eEdge.Weight);

                dataLast = vTable[prev.Index];
                prev = dataLast.Previous;
            }

            return result;
        }

        internal class PriorityQueue
        {
            private List<VertexData> list;

            public PriorityQueue()
            {
                list = new List<VertexData>();
            }


            //Return the lowest distance value in the queue
            internal VertexData Dequeue()
            {
                VertexData retVal = list[0];
                list.RemoveAt(0);
                return retVal;
            }

            internal void Enqueue(VertexData data)
            {
                list.Add(data);

                //Should do some research on the sorting algorithm done by a list
                //Currently, we have an ALMOST sorted list when sort is called.
                //Some algorithms are inefficient when operating on mostly sorted lists
                list.Sort();
            }

            public bool IsEmpty()
            {
                return (list.Count <= 0);
            }

            public void DisplayQueue()
            {
                foreach(VertexData vd in list)
                {
                    Console.WriteLine(vd.ToString());
                }

                Console.WriteLine();
            }
        }
        #endregion

        #region Abstract Methods
        //Helper method where the child class will insert the edge according to its own implementation
        protected abstract void AddEdge(Edge<T> e);

        //When adding a vertex, we need to tell the child class to make room for the edges of this vertex
        public abstract void AddVertexAdjustEdges(Vertex<T> v);

        public abstract void RemoveVertextAdjustEdges(Vertex<T> v);

        public abstract IEnumerable<Vertex<T>> EnumerateNeighbours(T data);

        public abstract Edge<T> GetEdge(T from, T to);

        public abstract bool HasEdge(T from, T to);

        public abstract void RemoveEdge(T from, T to);
        
        //Helper method for Minimum Spanning Tree
        protected abstract Edge<T>[] GetAllEdges();

        #endregion

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            //Loop through the vertices and add to the stringbuilder
            foreach (Vertex<T> v in EnumerateVertices())
            {
                result.Append(v + ", ");
            }
            //Take off the last comma
            if (vertices.Count > 0)
            {
                result.Remove(result.Length - 2, 2);
            }

            return this.GetType().Name + "\nVertices: " + result + "\n";
        }


    }
}
