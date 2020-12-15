using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deikstra
{
    class Program
    {
        static void ShowGraph(double[,] g)
        {
            for (var i = 0; i < g.GetLength(0); i++)
            {
                for (var j = 0; j < g.GetLength(1); j++)
                {
                    Console.Write("{0}\t", g[i, j]);
                }
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            var g = GraphLoader.Load("graf.csv");
            ShowGraph(g);
            Console.Write("Номер стартовой вершины: ");
            int b = -1, e = -1;
            int.TryParse(Console.ReadLine(), out b);
            Console.Write("Номер конечной вершины: ");
            int.TryParse(Console.ReadLine(), out e);
            if (b >= 0 && e >= 0)
            {
                Graph graph = new Graph(g);
                var path = graph.GetShortestPath(b, e);
                var length = Graph.GetShortestPathLength(path);
                Console.WriteLine(Graph.ShowPath(path));
                Console.WriteLine("Минимальная длина пути: {0}", length);
            }
        }
    }
}
