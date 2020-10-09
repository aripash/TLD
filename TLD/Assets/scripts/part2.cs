using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class part2 : MonoBehaviour
{
    public List<string> constraintList = new List<string>();
    static int numOfRoads = 0;
    List<float> density = new List<float>();
    float time = 0;
    bool flag = true;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > 2 && flag) 
        {
            flag = false;
            List<int> _int_den = new List<int>();
            foreach(float f in density)
            {
                _int_den.Add((int)(1000 / f));
            }
            bool[,] res = AlgoRun(numOfRoads, _int_den);
            Debug.Log(tools.DeepToString(ref res));
        }

    }
    public void addRoad(float cpm) 
    {
        numOfRoads++;
        density.Add(cpm);
    }

    /// <summary>
    /// Compute traffic lights cycle's order, MUST HAVE CONSTRAINTS IN PLACE!
    /// </summary>
    /// <param name="_nor"> Number of elements (roads or traffic lights) </param>
    /// <param name="_density"> Density of each element as list : (road number 1, density(0)),.... </param>
    /// <returns> Open order for elements based on constraints. each row represent cycles segment, 
    /// each column represents number of element to open. </returns>
    public bool[,] AlgoRun(int _nor, List<int> _density)
    {
        Constraints _cons = new Constraints(_nor);
        foreach (string s in constraintList)
        {
            string[] temp = s.Split(',');
            _cons.Add_cons(int.Parse(temp[0]), int.Parse(temp[1]));
        }
        Junction _jn = new Junction(_nor, _density.ToArray());
        Junction _result = SimulatedAnnealing.Compute(_jn, 20, 0.00001f,50);
        return _result.getOrder();
    }
}
