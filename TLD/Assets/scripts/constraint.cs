using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class constraint : MonoBehaviour
{
    public static List<string> cons=new List<string>();
    float time = 0;
    [SerializeField] GameObject algo = null;

    /// <summary>
    /// after 0.1 seconds(enough time for the roads to send thier info) send to the algorithm the constraint list and destroy this gameobject(unneeded)
    /// </summary>
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > 0.1)
        {
            algo.GetComponent<part2>().constraintList = cons;
        }
        if (time > 0.2) Destroy(this);
    }
    /// <summary>
    /// add a constraint(when 2 roads collide they create constraint) to a list
    /// </summary>
    /// <param name="con">string that represents the constraint</param>
    public void addcons(string con) {
        if (!cons.Contains(con))
        {
            //Debug.Log("collision  " + con);
            cons.Add(con);
        }   
    }
}
