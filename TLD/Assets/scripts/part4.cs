﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class part4 : MonoBehaviour
{
    public void lights(Junction res,int time)
    {
        float[] switchPercent = CycleSegmentsCompute(res);
        bool[,] routeSwitch = res.getOrder();
        //open lights(children) acording to res 
        int switches = switchPercent.Length;
        for(int i = 0; i < switches; i++)//iterate on each switch
        {
            //set active if routeSwitch[j,i] is true else deactive
            for (int j = 0; j < gameObject.transform.childCount; j++)
            {
                gameObject.transform.Find(j + "t").gameObject.SetActive(routeSwitch[j, i]);
            }
            //wait for the switch timer(switchPercent) to run out before the next switch
            for (float switchTimer = 0; switchTimer < switchPercent[i]; switchTimer += Time.deltaTime) ;

        }
        /*string s = gameObject.transform.Find("x").name; get a child named x
        string s = gameObject.transform.GetChild(i).name; get a child in position i*/
    }

    /// <summary>
    /// Calaculates segments distribution for cycle
    /// ***** _order[i] will activate for CycleTime * RESULT[i] *****
    /// </summary>
    /// <param name="_jn"> calculated Junction type </param>
    /// <returns> % for each segment </returns>
    private float[] CycleSegmentsCompute(Junction _jn)
    {
        int _segmentNumber = _jn.getOrder().GetLength(1); //how many segments for cycle
        float[] _percent = new float[_segmentNumber];  //the % for each segment of cycle
        int[] _den_sum = new int[_segmentNumber];  //the % for each segment of cycle
        bool[,] _order = _jn.getOrder();
        int _sum = 0;


        for (int i = 0; i < _order.GetLength(1); i++)   //sum of each segment
        {
            for (int j = 0; j < _order.GetLength(0); j++)
            {
                if (_order[j, i] == true)
                {
                    _den_sum[i] += _jn.getDensity()[j];
                }
            }
        }
        foreach (int n in _den_sum) //total sum
        {
            _sum += n;
        }
        for (int i = 0; i < _order.GetLength(1); i++)   //percent for each segment
        {
            _percent[i] = _den_sum[i] / _sum;
        }

        return _percent;
    }
}
