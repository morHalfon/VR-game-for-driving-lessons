/*Written by Mor Halfon 
 * 
 * This script is driving the car after the path.
 * The car is following the path by moving it's position and rotation angle.
 * By calculating the distance between two points on the path the car is moving
 * according to the defined speed.
*/



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCar : MonoBehaviour {

    public PathsEditor paths;
    public EditorPath pathToFollow;
    public int currWayPointID = 0;
    public float speed;
    private float reachDistance = 1.0f;
    public float rotationSpeed = 5.0f;
    public string pathName;
   

	// Use this for initialization
	void Start ()
    {
        pathToFollow = GameObject.Find(pathName).GetComponent<EditorPath>();
        paths = GameObject.Find("paths").GetComponent<PathsEditor>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(currWayPointID < pathToFollow.path_objs.Count)
        {
            float currDistance = Vector3.Distance(pathToFollow.path_objs[currWayPointID].position, transform.position);
            Vector3 target = pathToFollow.path_objs[currWayPointID].position;
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
            var rotation = Quaternion.LookRotation(target - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
            if (currDistance <= reachDistance)
            {
                currWayPointID++;
                if (currWayPointID == (pathToFollow.path_objs.Count - 1))
                {
                    Transform newPath = paths.chooseRandomPath(pathToFollow, pathToFollow.path_objs[currWayPointID]);
                    if (newPath != null)
                    {
                        pathName = newPath.name;
                        pathToFollow = GameObject.Find(pathName).GetComponent<EditorPath>();
                        currWayPointID = 0;
                    }
                   
                }

            }
        }
        
	}
}
