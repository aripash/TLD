using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dataAdapter : MonoBehaviour
{
    public List<string> constraintList = new List<string>();
    [SerializeField] int numOfRoads = 0;
    static List<float> density = new List<float>();
    [SerializeField] GameObject resultAdapter = null;
    int cycleTime = 300;
    [SerializeField] int timeForCycle = 20;
    //in case someone changed the cycleTime before the program started
    private void Start()
    {
        cycleTime = timeForCycle * numOfRoads;
        StartCoroutine(restart());

    }
    /// <summary>
    /// after a second we begin to calculate the algorithm(we wait enough time for the other parts to send all of thier relevant info)
    /// and after that we calculate the algorithm again every cycleTime seconds.
    /// </summary>
    public IEnumerator restart()
    {
        yield return new WaitForSeconds(2);
        List<int> _int_den = new List<int>();
        foreach (float f in density)
        {
            _int_den.Add((int)(f));
        }
        AlgoRun(numOfRoads, _int_den);
    }


    /// <summary>
    /// changes road's density
    /// </summary>
    /// <param name="name"> Road name </param>
    /// <param name="cpm"> Road's density (cars per minute) </param>
    public void changeRoad(int name, float cpm) 
    {
        //Debug.Log(tools.DeepToString(ref density));
        while (density.Count <= name)
        {
            density.Add(0f);
        }
        density[name] = cpm;
    }
    /// <summary>
    /// adds new road
    /// </summary>
    /// <param name="name"> Road name </param>
    /// <param name="cpm"> Road's density (cars per minute) </param>
    public void newRoad(int name, float cpm) 
    {
        changeRoad(name, cpm);
    }

    /// <summary>
    /// Compute traffic lights cycle's order, MUST HAVE CONSTRAINTS IN PLACE!
    /// </summary>
    /// <param name="_nor"> Number of elements (roads or traffic lights) </param>
    /// <param name="_density"> Density of each element as list : (road number 1, density(0)),.... </param>
    /// <returns> Open order for elements based on constraints. each row represent cycles segment, 
    /// each column represents number of element to open. </returns>
    public void AlgoRun(int _nor, List<int> _density)
    {
        cycleTime = numOfRoads * timeForCycle;
        Constraints _cons = new Constraints(_nor);
        foreach (string s in constraintList)
        {
            string[] temp = s.Split(',');
            _cons.Add_cons(int.Parse(temp[0]), int.Parse(temp[1]));
        }
        Junction _jn = new Junction(_nor, _density.ToArray());
        Junction _result = SimulatedAnnealing.Compute(_jn, 20, 0.00001f,3500);
        Debug.Log(_result);
        Debug.Log(tools.DeepToString(ref constraintList));
        Debug.Log(_result.Eval());
        StartCoroutine(resultAdapter.GetComponent<resultAdapter>().lights(_result,cycleTime));//send schedule instead
    }
    public void changeCycleTimer(string text) 
    {
        timeForCycle = int.Parse(text);
        cycleTime = timeForCycle * numOfRoads;
    }
}
