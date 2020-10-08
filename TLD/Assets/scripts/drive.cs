using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drive : MonoBehaviour
{
    [SerializeField] float speed = 1;
    public List<Vector3> gps;
    public int currentpoint = 0;
    public int maxpoints = 2;
    Rigidbody rb;
    [SerializeField]float safeDistance = 1;
    private static int serial=0;
    public GameObject parent;

    // Update is called once per frame
    void Update()
    {
        if (currentpoint > 0)
        {
            //avoid hitting cars and red trafficlights
            RaycastHit hit;
            Vector3 raycastpos = gameObject.transform.position;
            Vector3 dir = (gps[currentpoint] - gps[currentpoint - 1]).normalized;
            //cast a ray from raycastpos in the direction of movement at a safeDistance distance only on objects from the 8th layer and save to hit
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
            else
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
    public void firstSpeed() {
        rb = GetComponent<Rigidbody>();
        Vector3 dir = (gps[1] - gps[0]).normalized;
        rb.velocity = dir * speed;
        transform.LookAt(gps[1]);
        gameObject.name ="car no."+ serial++;
    }
/*
    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("car crash "+name+"  "+collision.gameObject.name);
    }*/
}
