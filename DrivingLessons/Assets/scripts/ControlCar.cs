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

    private PathsEditor all_paths;
    public EditorPath pathToFollow;
    private JunctionEditor all_junctions;
    private Transform nextPath;
    private int currWayPointID;
    private float speed;
    private float reachDistance;
    private float rotationSpeed;
    public string pathName;
    private bool inJunction;
   

	// Use this for initialization
	void Start ()
    {
        all_paths = GameObject.Find("paths").GetComponent<PathsEditor>();
        all_junctions = GameObject.Find("junctions").GetComponent<JunctionEditor>();
        pathToFollow = GameObject.Find(pathName).GetComponent<EditorPath>();
        speed = 20f;
        reachDistance = 1.0f;
        rotationSpeed = 5.0f;
        currWayPointID = 0;
        inJunction = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(currWayPointID < pathToFollow.path_objs.Count)
        {
            float currDistance = Vector3.Distance(pathToFollow.path_objs[currWayPointID].position, transform.position);
            Vector3 nextWayPoint = pathToFollow.path_objs[currWayPointID].position;
            setPositionAndRotation(nextWayPoint);
            
            if (currDistance <= reachDistance)
            {
                currWayPointID++;
                //slows the car is before an intersection.
                if (currWayPointID == (pathToFollow.path_objs.Count - 2) && !inJunction)
                {
                    speed = 10;
                }
                if (currWayPointID == (pathToFollow.path_objs.Count - 1))
                {
                    setNextPath();
                }

            }
        }
        
	}

    private void setPositionAndRotation(Vector3 nextPoint)
    {
        transform.position = Vector3.MoveTowards(transform.position, nextPoint, Time.deltaTime * speed);
        var rotation = Quaternion.LookRotation(nextPoint - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
    }
    private void setNextPath()
    {
        if (inJunction)
        {
            nextPath = all_paths.findPath(pathToFollow, pathToFollow.path_objs[currWayPointID]);
            if (nextPath != null)
            {
                pathName = nextPath.name;
                pathToFollow = GameObject.Find(pathName).GetComponent<EditorPath>();
                currWayPointID = 0;
                speed = 20;
            }
            inJunction = false;
        }
        else
        {
            nextPath = all_junctions.chooseRandomDirection(pathToFollow, pathToFollow.path_objs[currWayPointID]);
            if (nextPath != null)
            {
                pathName = nextPath.name;
                pathToFollow = GameObject.Find(pathName).GetComponent<EditorPath>();
                currWayPointID = 0;
            }
            inJunction = true;
        }
    }
}
