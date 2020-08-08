using System;
using System.Collections.Generic;
using System.Text;

namespace TLD
{
    class Constraints
    {

        static List<List<bool>> cons_lst;
        public Constraints()
        {
            cons_lst = new List<List<bool>>();
        }

        public void add_cons(List<bool> cons)
        {
            cons_lst.Add(cons);
        }

        public bool check_cons(int a, int b)
        {
            if (a > b)
                return cons_lst[a][b];
            return cons_lst[b][a];
        }
    }
}
