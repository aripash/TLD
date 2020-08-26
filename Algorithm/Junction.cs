using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace TLD
{
    /// <summary>
    /// Class represents junction with array as lane number (as given).
    /// Order represents which order lanes get green in traffic light cycle
    /// Density represents how dense each lane for Utility evaluation
    /// </summary>
    class Junction
    {
        int[] _order;
        int[] _density;
        static int CYCLE_SIZE = 4; //max cycle size
        private static Constraints cons;

        public Junction(int how_many_lanes, int[] _den)
        {
            _order = new int[how_many_lanes];
            _density = _den;
            cons = new Constraints(how_many_lanes);
        }

        public Junction(Junction original)
        {
            this._order = (int[])original._order.Clone();
            this._density = (int[])original._density.Clone();

        }

        /// <summary>
        /// Utility evaluation
        /// </summary>
        /// <returns> if result<0 : how many constraints are not satisfied
        /// if result > 0 : how good _order with given density </returns>
        public int Eval()
        {
            int result = 0;
            for (int i = 0; i < _order.Length; i++)
                for (int j = i + 1; j < _order.Length; j++)
                {
                    if (_order[i] == _order[j])
                        if (cons.Check_cons(i, j))
                            result--;
                }
            return result;
        }

        /// <summary>
        /// Randomly creates successor 
        /// </summary>
        /// <returns> random Successor Junction</returns>
        public Junction Choose_Random_Successor()
        {
            Random rand = new Random();
            Junction temp = new Junction(this);
            int tempRand = rand.Next(_order.Length);
            temp._order[tempRand] = rand.Next(CYCLE_SIZE);
            return temp;
        }

        public int[] getOrder()
        {
            return _order;
        }

        public override string ToString()
        {
            string res = "";
            res += "ORDER:   " + tools.DeepToString(_order) + "DENSITY:   " + tools.DeepToString(_density) + "EVAL:   " + Eval();
            return res; ;
        }
    }
}
