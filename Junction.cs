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
        List<int> _order;
        List<int> _density;
        static int CYCLE_SIZE; //max cycle size
        Constraints cons = new Constraints();

        Junction(int how_many_lanes)
        {
            _order = new List<int>();
            _density = new List<int>();
        }

        Junction(Junction original)
        {
            this._order = new List<int>(original._order);
            this._density = new List<int>(original._density);

        }

        /// <summary>
        /// Utility evaluation
        /// </summary>
        /// <returns> if result<0 : how many constraints are not satisfied
        /// if result > 0 : how good _order with given density </returns>
        public int Eval()
        {
            int result = 0;
            for(int i = 0; i < _order.Count; i++)
                for(int j = i + 1; i < _order.Count; j++)
                {
                    if (cons.check_cons(i, j))
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
            temp._order[rand.Next(_order.Count)] = rand.Next(CYCLE_SIZE);
            return temp;
        }
    }
}
