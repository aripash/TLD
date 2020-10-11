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
public class Junction
{
    bool[,] _order;
    int[] _density;
    //static int CYCLE_SIZE = 4; //max cycle size
    static Constraints cons;

    public Junction(int how_many_lanes, int[] _den)
    {
        _order = new bool[how_many_lanes, how_many_lanes];
        for (int i = 0; i < _order.GetLength(0); i++)
        {
            _order[i, i] = true;
        }
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
        if (result >= 0)
        {
            /* High number of lanes diversity */
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
            // result highest evaluation can be : number of lanes
            /* High number of lanes active at same time + high density release */
            int tempCounter = 0; // how many lanes active at the same time
            int tempDen = 0;
            if (result >= _order.GetLength(0))
            {
                for (int i = 0; i < _order.GetLength(1); i++)
                {
                    for (int j = 0; j < _order.GetLength(0); j++)
                    {
                        if (_order[j, i] == true)
                        {
                            tempCounter++;
                            tempDen += _density[j];
                        }
                    }
                    result += (tempCounter * tempCounter) + tempDen;
                    tempCounter = 0;
                    tempDen = 0;
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

    public int[] getDensity()
    {
        return _density;
    }

    public override string ToString()
    {
        string res = "";
        res += "ORDER:   \n" + tools.DeepToString(ref _order) + "DENSITY:   " + tools.DeepToString(ref _density) + "EVAL:   " + Eval();
        return res;
    }
}

