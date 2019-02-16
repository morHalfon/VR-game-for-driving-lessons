using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTurn : MonoBehaviour {

    float rotSpeed = 5;//20

    // Update is called once per frame
    void Update()
    {
        float rotx = Input.GetAxis("Mouse X") * rotSpeed * Mathf.Deg2Rad;
        float roty = Input.GetAxis("Mouse Y") * rotSpeed * Mathf.Deg2Rad;
        transform.RotateAround(Vector3.up, rotx);
        //transform.RotateAround(Vector3.right, roty);

       // Vector3 axis = new Vector3(-rotx, roty);
        //transform.RotateAround(axis, axis.magnitude * rotSpeed);

        //axis = new Vector3(-yDelta, xDelta)
        //RotateAround(axis, axis.magnitude * factor)

    }
}
