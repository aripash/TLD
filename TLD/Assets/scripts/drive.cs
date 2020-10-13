using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drive : MonoBehaviour
{
    [SerializeField] float speed = 1;//the speed of the car
    public List<Vector3> gps;//list of points the car needs to pass
    public int currentpoint = 0;//the current point the car is at
    public int maxpoints = 2;//number of maximum points the car needs to pass
    Rigidbody rb;
    [SerializeField]float safeDistance = 1;//the max distance an obstcle can be from the car
    public static int serial=0;//serial number
    public GameObject parent;//the road that spawned the car

    /// <summary>
    /// checks each frame what the car needs to do: stop, continue driving, change direction or finish.
    /// </summary>
    // Update is called once per frame
    void Update()
    {
        if (currentpoint > 0)
        {
            //avoid hitting cars and red trafficlights
            RaycastHit hit;
            Vector3 raycastpos = gameObject.transform.position;
            Vector3 dir = (gps[currentpoint] - gps[currentpoint - 1]).normalized;
            //cast a ray from raycastpos in the direction of movement at a safeDistance distance and save to hit
            if (Physics.Raycast(raycastpos, dir, out hit, safeDistance))
            {
                if (hit.collider.tag == "car") {
                    rb.velocity = Vector3.zero;
                    Vector3 currentPos = gameObject.transform.position;
                    if (Mathf.Abs(gps[currentpoint-1].x - currentPos.x) < 0.1f && Mathf.Abs(gps[currentpoint-1].z - currentPos.z) < 0.1f)
                    {
                        parent.GetComponent<roadBrain>().stop = true;
                    }
                }
            }
            else//if there is no hit continue as normal
            {
                rb.velocity = dir * speed;
                transform.LookAt(gps[currentpoint]);
                parent.GetComponent<roadBrain>().stop = false;
            }

            Vector3 currentGPS = gameObject.transform.position;
            //check if we got to the next point
            if (Mathf.Abs(gps[currentpoint].x - currentGPS.x) < 0.1f && Mathf.Abs(gps[currentpoint].z - currentGPS.z) < 0.1f)
            {
                gameObject.transform.position = gps[currentpoint];
                currentpoint++;
                //if we are done with the road
                if (currentpoint >= maxpoints) Destroy(gameObject);
                else
                {
                    dir = (gps[currentpoint] - gps[currentpoint - 1]).normalized;
                    rb.velocity = dir * speed;
                    transform.LookAt(gps[currentpoint]);
                }
            }

        }
    }
    /// <summary>
    /// initialize the speed and direction of the car after spawning but after it got its vars from the road that spawned it
    /// cant use start() for this
    /// </summary>
    public void firstSpeed() {
        rb = GetComponent<Rigidbody>();
        Vector3 dir = (gps[1] - gps[0]).normalized;
        rb.velocity = dir * speed;
        transform.LookAt(gps[1]);
        gameObject.name ="car no."+ serial++;
    }
    /*
     * for testing Purpose
        public void OnCollisionEnter(Collision collision)
        {
            Debug.Log("car crash "+name+"  "+collision.gameObject.name);
        }*/
}
