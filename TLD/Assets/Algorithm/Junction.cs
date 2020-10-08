using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using UnityEngine;

/// <summary>
/// Class represents junction with array as lane number (as given).
/// Order represents which order lanes get green in traffic light cycle
/// Density represents how dense each lane for Utility evaluation
/// </summary>
class Junction
{
    bool[,] _order;
    int[] _density;
    static int CYCLE_SIZE = 4; //max cycle size
    private static Constraints cons;

    public Junction(int how_many_lanes, int[] _den)
    {
        _order = new bool[how_many_lanes, CYCLE_SIZE];
        _density = _den;
        cons = new Constraints(how_many_lanes);
    }

    public Junction(Junction original)
    {
        this._order = (bool[,])original._order.Clone();
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
        /** Negative check: go through every col -> find #t in col -> check for other #t in same col for constraints
         * each constraint -1 Utilily **/
        for (int i = 0; i < _order.GetLength(1); i++)
        {
            for (int j = 0; j < _order.GetLength(0); j++)
            {
                if (_order[j, i])
                {
                    for (int k = j + 1; k < _order.GetLength(0); k++)
                    {
                        if (_order[k, i])
                        {
                            if (cons.Check_cons(k, j))
                                result--;
                        }
                    }
                }
            }
        }
        /* High number of lanes diversity */
        if (result >= 0)
        {
            for (int i = 0; i < _order.GetLength(0); i++)
            {
                for (int j = 0; j < _order.GetLength(1); j++)
                {
                    if (_order[i, j] == true)
                    {
                        result++;
                        j = _order.GetLength(1);
                    }
                }
            }

            /* High number of lanes active at same time */
            int tempCounter = 0;
            if (result >= _order.GetLength(0) - 1)
            {
                for (int i = 0; i < _order.GetLength(1); i++)
                {
                    for (int j = 0; j < _order.GetLength(0); j++)
                    {
                        if (_order[j, i] == true)
                        {
                            tempCounter++;
                        }
                    }
                    result += (tempCounter * tempCounter);
                }

                /* High load release? */
                int tempCounterDensity = 0;
                for (int i = 0; i < _order.GetLength(0); i++)
                {
                    for (int j = 0; j < _order.GetLength(1); j++)
                    {
                        if (_order[i, j] == true)
                        {
                            tempCounterDensity += _density[i];
                        }
                    }
                    result += tempCounterDensity * tempCounterDensity;
                }
            }
        }

        return result;
    }

    /// <summary>
    /// Randomly creates successor 
    /// </summary>
    /// <returns> random Successor Junction</returns>
    public Junction Choose_Random_Successor()
    {
        System.Random rand = new System.Random();
        Junction temp = new Junction(this);
        int tempRandRow = rand.Next(_order.GetLength(0));
        int tempRandCol = rand.Next(_order.GetLength(1));
        temp._order[tempRandRow, tempRandCol] = !temp._order[tempRandRow, tempRandCol];
        return temp;
    }

    public bool[,] getOrder()
    {
        return _order;
    }

    public override string ToString()
    {
        string res = "";
        res += "ORDER:   \n" + tools.DeepToString(ref _order) + "DENSITY:   " + tools.DeepToString(ref _density) + "EVAL:   " + Eval();
        return res;
    }
}

