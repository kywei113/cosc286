using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graphs;


namespace GraphMatrix
{
    class Program
    {
        static void ProcessCity(string s)
        {
            Console.WriteLine(s);
        }

        static void TestDepthFirst(UGraphMatrix<string> g, string s)
        {
            Console.WriteLine("Depth First");
            g.DepthFirstTraversal(s, ProcessCity);
        }

        static void TestBreadthFirst(UGraphMatrix<string> g, string s)
        {
            Console.WriteLine("Breadth First");
            g.BreadthFirstTraversal(s, ProcessCity);
        }

        static void TestMinimumSpanningTree(UGraphMatrix<string> g)
        {
            Console.WriteLine("Minimum Spanning Tree");
            UGraphMatrix<string> minGraph = (UGraphMatrix<string>)g.MinimumSpanningTree();
            Console.WriteLine(minGraph);
        }

        static void TestEnumerateNeighbours(UGraphMatrix<string> g, string start)
        {
            Console.WriteLine("Enumerate Neighbours");
            foreach (Vertex<string> vertex in g.EnumerateNeighbours(start))
            {
                Console.WriteLine(vertex);
            }
        }

        static void Main(string[] args)
        {
            UGraphMatrix<string> uGraph = new UGraphMatrix<string>();
            
            AddData(uGraph);

            Console.WriteLine(uGraph);

            TestMinimumSpanningTree(uGraph);
            TestEnumerateNeighbours(uGraph, "Reg");
            TestDepthFirst(uGraph, "SA");
            TestBreadthFirst(uGraph, "SA");
        }

        static void AddData(UGraphMatrix<string> g)
        {
            g.AddVertex("SA");
            g.AddVertex("PA");
            g.AddVertex("Reg");
            g.AddVertex("MJ");
            g.AddVertex("Wyn");
            g.AddVertex("Yk");
            g.AddVertex("SC");
            g.AddVertex("Wey");

            g.AddEdge("SA", "PA", 140);
            g.AddEdge("SA", "Reg", 250);
            g.AddEdge("SA", "SC", 300);
            g.AddEdge("MJ", "Reg", 72);
            g.AddEdge("MJ", "SC", 200);
            g.AddEdge("SA", "Wyn", 189);
            g.AddEdge("Wyn", "Yk", 140);
            g.AddEdge("Reg", "Yk", 190);
            g.AddEdge("Wey", "Reg", 116);
        }
    }


}
