using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createRoad : MonoBehaviour
{
    [SerializeField] GameObject road=null;
    bool startRoad = false;
    List<Vector3> roadmap= new List<Vector3>();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) {
            if (roadmap.Count > 1) {
               for(int i = 0; i < roadmap.Count-1; i++) {

                    Vector3 pointstart = roadmap[i];
                    Vector3 pointend = roadmap[i+1];
                    Vector3 pointmiddle = Vector3.Lerp(pointstart, pointend, 0.5f);
                    float length = Mathf.Sqrt(Mathf.Pow(Mathf.Abs(pointstart.x - pointend.x), 2) + Mathf.Pow(Mathf.Abs(pointend.z - pointstart.z), 2));
                    GameObject newroad = Instantiate(road, pointmiddle, road.gameObject.transform.rotation);
                    newroad.transform.localScale = new Vector3(length, 0.2f, 1);
                    float ang = Mathf.Atan2(Math.Abs(pointend.z - pointstart.z), Math.Abs(pointstart.x - pointend.x)) * Mathf.Rad2Deg;
                    Vector3 Angle = new Vector3(0, ang, 0);
                    newroad.transform.localEulerAngles = Angle;
                }
            }
            roadmap = new List<Vector3>();
            startRoad = !startRoad;
        }
        if (startRoad)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit = new RaycastHit();
                Physics.Raycast(ray, out hit);
                Vector3 pos = hit.point;
                pos.y = 0;
                roadmap.Add(pos);
            }
        }
    }
}


/*
Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
RaycastHit hit = new RaycastHit();
Physics.Raycast(ray, out hit);
                Vector3 pos = hit.point;
pos.y = 0;
                Instantiate(road, pos, road.gameObject.transform.rotation);*/