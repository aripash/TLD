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

    // Update is called once per frame
    void Update()
    {
        if (currentpoint > 0)
        {
               //avoid hitting cars and red trafficlights
               RaycastHit hit;
               Vector3 raycastpos = transform.position;
               raycastpos.y += 0.25f;
               Vector3 dir = (gps[currentpoint] - gps[currentpoint - 1]).normalized;
               //cast a ray from raycastpos in the direction of movement at a safeDistance distance only on objects from the 8th layer and save to hit
               if (Physics.Raycast(raycastpos, dir, out hit, safeDistance))
               {
                   if (hit.collider.tag == "car") { rb.velocity = Vector3.zero; }             
               }
                else
                {
                    rb.velocity = dir * speed;
                }
            //allways look at the next point
            transform.LookAt(gps[currentpoint]);

            Vector3 currentGPS = gameObject.transform.position;
            //check if we got to the next point
            if (Mathf.Abs(gps[currentpoint].x - currentGPS.x) < 0.1f && Mathf.Abs(gps[currentpoint].z - currentGPS.z) < 0.1f)
            {
                currentpoint++;
                if (currentpoint >= maxpoints) Destroy(gameObject);
                else
                {
                    dir = (gps[currentpoint] - gps[currentpoint - 1]).normalized;
                    rb.velocity = dir * speed;
                }
            }
            
        }
    }
    public void firstSpeed() {
        rb = GetComponent<Rigidbody>();
        Vector3 dir = (gps[1] - gps[0]).normalized;
        rb.velocity = dir * speed;
        transform.LookAt(gps[1]);
    }
}
