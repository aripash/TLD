using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class constraint : MonoBehaviour
{
    public static List<string> cons=new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// add a constraint to a list
    /// </summary>
    /// <param name="con">string that represents the constraint</param>
    public void addcons(string con) {
        Debug.Log("collision  " + con);
        cons.Add(con);
        
    }
}
