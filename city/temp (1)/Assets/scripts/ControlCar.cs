using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCar : MonoBehaviour {

    public EditorPath pathToFollow;
    public int currWayPointID = 0;
    public float speed;
    private float reachDistance = 1.0f;
    public float rotationSpeed = 5.0f;
    public string pathName;

    Vector3 lastPos;
    Vector3 currPos;

	// Use this for initialization
	void Start ()
    {
        pathToFollow = GameObject.Find(pathName).GetComponent<EditorPath>();
        lastPos = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        float distance = Vector3.Distance(pathToFollow.path_objs[currWayPointID].position, transform.position);
        Vector3 target = pathToFollow.path_objs[currWayPointID].position;
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
        var rotation = Quaternion.LookRotation(target - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);

        if(distance <= reachDistance)
        {
            currWayPointID++;
        }
	}
}
