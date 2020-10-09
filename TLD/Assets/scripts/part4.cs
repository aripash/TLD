using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class part4 : MonoBehaviour
{
    // Start is called before the first frame update
    public void lights(Junction res)
    {
        //open lights acording to res
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
