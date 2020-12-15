using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Deikstra
{
    class Vertex
    {
        private static int count = 0;

        public int Id
        {
            get;
            private set;
        }

        public double Label
        {
            get;
            private set;
        }

        public bool Visited;

        public Vertex Previous
        {
            get;
            private set;
        }

        public Vertex()
        {
            Id = count;
            count++;
            Previous = null;
            Visited = false;
            Label = Double.PositiveInfinity;
        }

        public void SetLabel(Vertex vrt, double d)
        {
            var s = vrt.Label + d;
            if (Label > s)
            {
                Label = s;
                Previous = vrt;
            }
        }

        public void SetStart()
        {
            Label = 0;
        }
    }
    
    class Graph
    {
        private double[,] g;
        private List<Vertex> vrts;
        
        public Graph(double[,] g)
        {
            this.g = g;
            vrts = new List<Vertex>();
            for (int i = 0; i < g.GetLength(0); i++)
            {
                vrts.Add(new Vertex());
            }
        }

        public List<Vertex> GetShortestPath(int b, int e)
        {
            vrts[b].SetStart();
            var currId = GetNextVertexId();
            //Stack<Vertex> s = new Stack<Vertex>();
            while (currId >= 0)
            {
                //s.Push(vrts[currId]);
                for (int i = 0; i < vrts.Count; i++)
                {
                    if (i == currId) continue;
                    if (Math.Abs(g[currId, i]) > Double.Epsilon)
                    {
                        vrts[i].SetLabel(vrts[currId], g[currId, i]);
                    }
                }
                vrts[currId].Visited = true;
                currId = GetNextVertexId();
            }

            var result = new List<Vertex>();
            /*while (s.Count > 0)
            {
                result.Add(s.Pop());
            }*/
            result.Add(vrts[e]);
            while (result[result.Count - 1].Previous != null)
            {
                result.Add(result[result.Count-1].Previous);
            }
            result.Reverse();
            return result;
        }

        private int GetNextVertexId()
        {
            double minLabel = Double.PositiveInfinity;
            int vId = -1;
            foreach (var v in vrts)
            {
                if (!v.Visited && v.Label < minLabel)
                {
                    minLabel = v.Label;
                    vId = v.Id;
                }
            }

            return vId;
        }

        public static double GetShortestPathLength(List<Vertex> path)
        {
            return path[path.Count - 1].Label;
        }

        public static String ShowPath(List<Vertex> path)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var v in path)
            {
                sb.Append(v.Id);
                sb.Append(" -> ");
            }

            sb.Append("|");
            return sb.ToString();
        }
    }
}
