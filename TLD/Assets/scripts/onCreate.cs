using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class onCreate : MonoBehaviour
{
    constraint constraints;
   // [SerializeField] int amountOfCars;
    static int serialnumber=0;
    [SerializeField] float secondsPerCar=9999;
    [SerializeField] GameObject startRoad=null;
    [SerializeField] GameObject middleRoad=null;
    [SerializeField] GameObject endRoad=null;
    [SerializeField] GameObject car=null;
    float secondNumber = 0;
    List<Vector3> list;
    int mid = 0;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.name = ""+serialnumber++;
        constraints = GameObject.Find("constraints").GetComponent<constraint>();
        list = new List<Vector3>();
        list.Add(startRoad.transform.position);
        if (middleRoad != null) { list.Add(middleRoad.transform.position);
            mid = 1;
        }
        list.Add(endRoad.transform.position);
       
     //   car.GetComponent<drive>().gps = list;
   //     car.GetComponent<drive>().maxpoints=mid+2;
    }

    // Update is called once per frame
    void Update()
    {
        secondNumber+=Time.deltaTime;
        if ((int)secondNumber >= secondsPerCar)
        {
            createCars();
        }
    }

    /// <summary>
    /// when you add a road, it needs to update the constraints when it touches another road
    /// activates automaticly
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "road") { 
        constraints.addcons(gameObject.name + "," + collision.gameObject.name);
    }
        
    }
    /// <summary>
    /// creates cars on the road, the cars will drive from start to end and through middle if it exists
    /// </summary>
    private void createCars()
    {
        secondNumber = 0;
        GameObject newCar = Instantiate(car, list[0], car.transform.rotation);
        newCar.GetComponent<drive>().gps = list;
        newCar.GetComponent<drive>().maxpoints = mid + 2;
        newCar.GetComponent<drive>().firstSpeed();
        newCar.GetComponent<drive>().currentpoint = 1;
    }

}
