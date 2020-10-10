using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trafficBrain : MonoBehaviour
{
    //private static int number = 0;
    [SerializeField] GameObject parent = null;
    // Start is called before the first frame update
    void Start()
    {
        //gameObject.name="traffic light no."+number++;
        //add to parent as child
        gameObject.transform.SetParent(parent.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
