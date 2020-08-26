using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace TLD
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("C:\\Users\\igork\\source\\repos\\TLD\\TLD\\tests.txt");
            string[] _density = lines[1].Split(",");
            List<int> _den = new List<int>();
            foreach (string d in _density)
                _den.Add(int.Parse(d));
            Junction jn = new Junction(int.Parse(lines[0]),_den.ToArray());
            Constraints cons = new Constraints(int.Parse(lines[0]));
            string[] temp = lines[2].Split("|");
            foreach(string s in temp)
            {
                string[] p = s.Split(",");
                cons.Add_cons(int.Parse(p[0]),int.Parse(p[1]));
            }
            jn = SimulatedAnnealing.Compute(jn, 20f, 0.000001f, 50);
           // Console.WriteLine(tools.DeepToString(_den.ToArray()));
            Console.WriteLine(jn);
            //for(int i = 0; i < jn.getOrder().Length; i++) {
            //    Console.Write(" " + jn.getOrder()[i]);
            //}



        }
    }
}
