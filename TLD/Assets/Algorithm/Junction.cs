﻿using System;
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
    bool[,] _order; // green lights order
    int[] _density; // density for each lane
    static Constraints cons;

    public Junction(int how_many_lanes, int[] _den)
    {
        _order = new bool[how_many_lanes, how_many_lanes];
        _density = _den;
        cons = new Constraints(how_many_lanes);
        // make decent starting point
        for (int i = 0; i < how_many_lanes; i++)
        {
            _order[i, i] = true;
            if (cons.numberOfCons(i) == 0)
            {
                for (int j = 0; j < how_many_lanes; j++)
                {
                    _order[i, j] = true;
                }
            }
        }
    }

    /// <summary>
    /// Clone initiator
    /// </summary>
    /// <param name="original"> Original Junction </param>
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
            //int[] _denClone = (int[])_density.Clone();
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
                    result += tempCounter + (tempDen * tempDen);
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
    /// <summary>
    /// Returns _Order
    /// </summary>
    /// <returns> bool[,] _Order </returns>
    public bool[,] getOrder()
    {
        return _order;
    }
    /// <summary>
    /// Manualy set order and density
    /// </summary>
    /// <param name="ord"> new Order </param>
    /// <param name="den"> new Density </param>
    public void setNewOrderDen(bool[,] ord, int[] den)
    {
        _order = ord;
        _density = den;
    }
    /// <summary>
    /// Returns _density
    /// </summary>
    /// <returns> int[] _density </returns>
    public int[] getDensity()
    {
        return _density;
    }

    /// <summary>
    /// To string
    /// </summary>
    /// <returns> 
    /// Order:
    /// Density:
    /// Eval:
    /// </returns>
    public override string ToString()
    {
        string res = "";
        res += "ORDER:   \n" + tools.DeepToString(ref _order) + "DENSITY:   " + tools.DeepToString(ref _density) + "EVAL:   " + Eval();
        return res;
    }
}

