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

    // Update is called once per frame
    void Update()
    {
        if (currentpoint > 0) {
            Vector3 currentGPS = gameObject.transform.position;
            if (Mathf.Abs(gps[currentpoint].x - currentGPS.x) < 0.1f && Mathf.Abs(gps[currentpoint].z - currentGPS.z) < 0.1f)
            {
                currentpoint++;
                if (currentpoint >= maxpoints) Destroy(gameObject);
                else
                {
                    Vector3 dir = (gps[currentpoint] - gps[currentpoint - 1]).normalized;
                    rb.velocity = dir * speed;
                    int degree = 180;
                    if (dir.x != 0)
                    {
                        degree = 90;
                        if (dir.x > 0) degree =-90;
                    }
                    else if (dir.z > 1) degree = 0;
                    rb.transform.rotation = Quaternion.Euler(0, degree, 0);
                }
            }
        }
    }
    public void firstSpeed() {
        rb = GetComponent<Rigidbody>();
        Vector3 dir = (gps[1] - gps[0]).normalized;
        rb.velocity = dir * speed;
        int degree = 180;
        if (dir.x != 0) {
            degree = 90;
            if (dir.x > 0) degree *= -1;
        }
        else if (dir.z > 1) degree = 0;
        rb.transform.rotation = Quaternion.Euler(0, degree, 0);
    }
}
