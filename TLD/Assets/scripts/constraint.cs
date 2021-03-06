﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class constraint : MonoBehaviour
{
    public static List<string> cons = null;//list with the constraints
    float time = 0;//how much time has passed
    [SerializeField] GameObject algo = null;//object that holds the data adaptor
    bool flag = false;//flag to stop sending the list

    private void Start()
    {

        cons = new List<string>();
    }
    /// <summary>
    /// after 0.1 seconds(enough time for the roads to send thier info) send to the algorithm the constraint list and destroy this gameobject(unneeded)
    /// </summary>
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > 1&&!flag)
        {
            algo.GetComponent<dataAdapter>().constraintList = cons;
            tools.cons = cons;
            flag = true;
        }

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
