
using Packages.Rider.Editor.Util;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class roadBrain : MonoBehaviour
{
    constraint constraints;
    //static int serialnumber=0;
    [SerializeField] float secondsPerCar=9999;
    [SerializeField] GameObject startRoad=null;
    [SerializeField] GameObject middleRoad=null;
    [SerializeField] GameObject endRoad=null;
    [SerializeField] GameObject car=null;
    float secondNumber = 0;
    List<Vector3> list;
    int mid = 0;
    public bool stop = false;
    [SerializeField] InputField inpf = null;//original
    private InputField iF;//copy for a specific road
    [SerializeField] GameObject algo = null;
    
    void Start()
    {
        iF = Instantiate(inpf);
        iF.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform);
        iF.onValueChanged.AddListener(delegate { newCPM(iF.text); });
        iF.placeholder.GetComponent<Text>().text +=""+ gameObject.name;
        iF.transform.position = new Vector3(125, 500 - 25 * int.Parse(gameObject.name), 0);
        constraints = GameObject.Find("constraints").GetComponent<constraint>();
        list = new List<Vector3>();
        list.Add(startRoad.transform.position);
        if (middleRoad != null) { list.Add(middleRoad.transform.position);
            mid = 1;
        }
        list.Add(endRoad.transform.position);
        algo.GetComponent<part2>().addRoad(int.Parse(gameObject.name), secondsPerCar);
    }

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
        newCar.GetComponent<drive>().parent = gameObject;
        newCar.GetComponent<drive>().firstSpeed();
        newCar.GetComponent<drive>().currentpoint = 1;
    }
    /// <summary>
    /// Updates new density
    /// </summary>
    /// <param name="cpm"> cars per minute </param>
    public void newCPM(string cpm)
    {
        int cpmt = int.Parse(cpm);
        secondsPerCar = 60 / cpmt;
        if (secondsPerCar < 1) secondsPerCar = 1;
        algo.GetComponent<part2>().addRoad(int.Parse(gameObject.name), secondsPerCar);
    }
}
