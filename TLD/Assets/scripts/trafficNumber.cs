using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trafficNumber : MonoBehaviour
{
    private static int number = 0;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.name="traffic light no."+number++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
