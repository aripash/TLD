using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class part2 : MonoBehaviour
{
    public List<string> constraintList = new List<string>();
    int numOfRoads = 0;
    List<float> density = new List<float>();
    float time = 0;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > 0.25) 
        {
            
        }
    }
    public void addRoad(float cpm) 
    {
        numOfRoads++;
        density.Add(cpm);
    }
}
