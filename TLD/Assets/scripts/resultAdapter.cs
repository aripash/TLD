using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resultAdapter : MonoBehaviour
{
    /// <summary>
    /// we open and close the traffic lights according to the results we got from the algorithm
    /// </summary>
    /// <param name="res">the results we get after the algorithm finished</param>
    public IEnumerator lights(Junction res, float totalTime)
    {
        float[] switchPercent = CycleSegmentsCompute(res);
        bool[,] routeSwitch = res.getOrder();
        //open lights(children) acording to res 
        int switches = switchPercent.Length;
        for(int i = 0; i < switches; i++)//iterate on each switch
        {
            //close all roads first then wait a second and then open the roads
            for (int j = 0; j < gameObject.transform.childCount; j++)
            {
                gameObject.transform.Find(j + "t").gameObject.SetActive(true);
            }
            //yellow light
            yield return new WaitForSeconds(2);

            for (int j = 0; j < gameObject.transform.childCount; j++)
            {
                if(routeSwitch[j, i])
                    gameObject.transform.Find(j + "t").gameObject.SetActive(false);
            }

            //wait for the switch timer(switchPercent) to run out before the next switch
            yield return new WaitForSeconds(switchPercent[i] * totalTime);

        }
        StartCoroutine(GameObject.Find("Algo").GetComponent<dataAdapter>().restart());
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
        float[] _den_sum = new float[_segmentNumber];  //the % for each segment of cycle
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
            if(_sum != 0)
                _percent[i] = _den_sum[i] / _sum;
        }

        return _percent;
    }
    /// <summary>
    /// stops the cycle
    /// </summary>
    public void end()
    {
        StopAllCoroutines();
    }
    /// <summary>
    /// applies the cycle on the trafficlights
    /// </summary>
    /// <param name="res">the resulting junction from running the algo</param>
    /// <param name="totalTime">the total time the cycle runs</param>
    public void schedule(Junction res, float totalTime)
    {
        StartCoroutine(lights(res, totalTime));
    }
    /// <summary>
    /// restarts the cycle
    /// </summary>
    public void restart()
    {
        StartCoroutine(GameObject.Find("Algo").GetComponent<dataAdapter>().restart());
    }

    /// <summary>
    /// sort by density - Higher dense segment will go first
    /// </summary>
    /// <param name="_jn"></param>
    /// <param name="per"></param>
    public void sortByDensity(Junction _jn, float[] per)
    {
        float maxPer = 0;
        int index = 0;
        bool[,] order = _jn.getOrder();
        int[] den = _jn.getDensity();
        for(int i = 0; i < per.Length; i++)
        {
            for (int j = i; j < per.Length; j++)
            {
                if(maxPer < per[j])
                {
                    maxPer = per[j];
                    index = j;
                }
            }
            tools.SwapColumns(ref order, i, index);
            tools.SwapArr(ref den, i, index);
            maxPer = 0;
            index = 0;
        }
        _jn.setNewOrderDen(order, den);
    }
}
