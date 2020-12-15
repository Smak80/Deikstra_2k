using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deikstra
{
    class GraphLoader
    {
        public static double[,] Load(String filename)
        {
            var f = new StreamReader(filename);
            string line;
            double[,] edges = null;
            int i = 0;
            int size = 0;
            while (!f.EndOfStream)
            {
                line = f.ReadLine();
                var sEdges = line.Split(new[] {';'});
                size = sEdges.Length;
                if (edges==null) edges = new double[size,size];
                for (int j = 0; j < size; j++)
                {
                    try
                    {
                        edges[i, j] = double.Parse(sEdges[j]);
                    }
                    catch (FormatException e)
                    {
                        if (sEdges[j].Contains('.'))
                        {
                            double.TryParse(sEdges[j].Replace('.', ','), out edges[i, j]);
                        }
                        else
                        {
                            double.TryParse(sEdges[j].Replace(',', '.'), out edges[i, j]);
                        }
                    }
                }

                i++;
            }
            f.Close();
            if (i == size) return edges;
            throw new Exception("Ошибка в формате файла");
        }
    }
}
