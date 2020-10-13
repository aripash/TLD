
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class roadBrain : MonoBehaviour
{
    constraint constraints;//constraint object that contains the constraint list
    //static int serialnumber=0;
    [SerializeField] float secondsPerCar=9999;//how much time needs to pass before we can spawn a car
    [SerializeField] GameObject startRoad=null;//first point
    [SerializeField] GameObject middleRoad=null;//second point
    [SerializeField] GameObject endRoad=null;//last point
    [SerializeField] GameObject car=null;//prefab of the car that the road spawns
    float secondNumber = 0;//how much time has passed
    List<Vector3> list;//list of all the points
    int mid = 0;//indicates if we have a middle or not
    public bool stop = false;//flag to stop spawning cars
    [SerializeField] InputField inpf = null;//original inputfield
    private InputField iF;//copy for a specific road
    [SerializeField] GameObject algo = null;//gameobject that holds the dataAdaptor
    
    void Start()
    {
        //make an interactable text field, and connect it to the code
        iF = Instantiate(inpf);
        iF.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform);
        iF.onEndEdit.AddListener(delegate { newCPM(iF.text); });
        iF.placeholder.GetComponent<Text>().text +=""+ gameObject.name;
        iF.transform.position = new Vector3(125, 500 - 25 * int.Parse(gameObject.name), 0);

        //find the constraint list to be able to add to it in the future
        constraints = GameObject.Find("constraints").GetComponent<constraint>();

        //make a list of the driving points to give the cars later
        list = new List<Vector3>();
        list.Add(startRoad.transform.position);
        if (middleRoad != null) { list.Add(middleRoad.transform.position);
            mid = 1;
        }
        list.Add(endRoad.transform.position);

        //send to the algorithm the road's details
        algo.GetComponent<dataAdapter>().newRoad(int.Parse(gameObject.name), 60/secondsPerCar);
    }

    /// <summary>
    /// spawn a car at the starting point every secondsPerCar
    /// </summary>
    // Update is called once per frame
    void Update()
    {
        if (!stop)
        {
            secondNumber += Time.deltaTime;
            if ((int)secondNumber >= secondsPerCar)
            {
                createCars();
            }
        }
    }

    /// <summary>
    /// when the roads intersect, we need to add that to the list of constraints for the algorithm
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "road") { 
            constraints.addcons(gameObject.name + "," + collision.gameObject.name);
        }
    }
        
    /// <summary>
    /// creates cars on the road, the cars will drive from start to end and through middle if it exists.
    /// </summary>
    private void createCars()
    {
        secondNumber = 0;
        GameObject newCar = Instantiate(car, list[0], car.transform.rotation);
        newCar.GetComponent<drive>().gps = list;
        newCar.GetComponent<drive>().maxpoints = mid + 2;
        newCar.GetComponent<drive>().parent = gameObject;
        newCar.GetComponent<drive>().firstSpeed();
        newCar.GetComponent<drive>().currentpoint = 1;
    }
    /// <summary>
    /// Updates new density and send the new info to the algorithm
    /// </summary>
    /// <param name="cpm"> cars per minute </param>
    public void newCPM(string cpm)
    {
        int cpmt = int.Parse(cpm);
        if (cpmt < 1|| cpmt > 60)
            secondsPerCar = 1;
        else secondsPerCar =60/cpmt;
        //after changing the road's density we need to restart the algo
        algo.GetComponent<dataAdapter>().changeRoad(int.Parse(gameObject.name), 60/secondsPerCar);
        StartCoroutine( algo.GetComponent<dataAdapter>().restart());
    }
}
