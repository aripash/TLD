using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class part2 : MonoBehaviour
{
    public List<string> constraintList = new List<string>();
    static int numOfRoads = 0;
    List<float> density = new List<float>();
    float time = 39;
    [SerializeField] GameObject part4 = null;
    [SerializeField] int cycleTime = 40;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > cycleTime ) 
        {
            time = 0;
            List<int> _int_den = new List<int>();
            foreach(float f in density)
            {
                _int_den.Add((int)(1000 / f));
            }
            AlgoRun(numOfRoads, _int_den);
            
            //Debug.Log(tools.DeepToString(ref res));
        }

    }

    /// <summary>
    /// changes road's density
    /// </summary>
    /// <param name="name"> Road name </param>
    /// <param name="cpm"> Road's density (cars per minute) </param>
    public void changeRoad(int name, float cpm) 
    {

        if (density.Count > name)
        {
            density.Insert(name, cpm);
        }
        else
        {
            density.Capacity = name + 1;
            density.Insert(name, cpm);
        }
    }
    /// <summary>
    /// adds new road
    /// </summary>
    /// <param name="name"> Road name </param>
    /// <param name="cpm"> Road's density (cars per minute) </param>
    public void newRoad(int name, float cpm) 
    {
        numOfRoads++;
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
        Constraints _cons = new Constraints(_nor);
        foreach (string s in constraintList)
        {
            string[] temp = s.Split(',');
            _cons.Add_cons(int.Parse(temp[0]), int.Parse(temp[1]));
        }
        Junction _jn = new Junction(_nor, _density.ToArray());
        Junction _result = SimulatedAnnealing.Compute(_jn, 20, 0.00001f,50);
        part4.GetComponent<part4>().lights(_result, cycleTime);//send schedule instead
    }
}
