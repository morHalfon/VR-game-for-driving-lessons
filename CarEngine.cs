using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEngine : MonoBehaviour {

    public Transform path;
    public float maxSteerAngle = 40f;
    public WheelCollider wheelFL;
    public WheelCollider wheelFR;
    public WheelCollider wheelRL;
    public WheelCollider wheelRR;
    private List<Transform> nodes;
    private int currentNode = 0;
    public float maxMotorTorque= 450f;
    public float maxBrakeTorque = 600f;
    public float currentSpeed;//=1000f;
    public float maxSpeed=1000f;
    public float changeAngle = 2f;
    public Vector3 centerOfMass;
    public bool isBraking = false;
    public Texture2D textureNormal;
    public Texture2D textureBraking;
    public Renderer carRenderer;
    Rigidbody rigidbody;

    // Use this for initialization
    void Start () {
        currentSpeed = 10f;
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.centerOfMass = centerOfMass;
        Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();
        
        for (int i = 0; i < pathTransforms.Length; i++)
        {
            if (pathTransforms[i] != path.transform)
            {
                nodes.Add(pathTransforms[i]);
            }

        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        ApplySteer();
        Drive();
        checkeWayPointDistance();
        Braking();
	}

    private void ApplySteer()
    {
        Vector3 relVec=transform.InverseTransformPoint(nodes[currentNode].position);
        //print(relVec);
        //relVec = relVec / relVec.magnitude;
        float newSteer = (relVec.x / relVec.magnitude) * maxSteerAngle;
        wheelFL.steerAngle = newSteer;
        wheelFR.steerAngle = newSteer;
    }
    private void Drive()
    {
     //    rigidbody.MovePosition(rigidbody.position + (nodes[currentNode + 1].position - nodes[currentNode].position).normalized * currentSpeed * Time.fixedDeltaTime);
        currentSpeed = 2 * Mathf.PI * wheelFL.radius * wheelFL.rpm * 60 / 1000;
       // print("currentNode " + currentNode);
        if (currentSpeed < maxSpeed && !isBraking)
        {
            wheelFL.motorTorque = maxMotorTorque;
            wheelFR.motorTorque = maxMotorTorque;
        }
        else
        {
            
            wheelFL.motorTorque = 0;
            wheelFR.motorTorque = 0;
        }

    }
    private void checkeWayPointDistance()
    {
        
        
        if (Vector3.Distance(transform.position,nodes[currentNode].position)< changeAngle)
        {
            
            if (currentNode == nodes.Count - 1)
                currentNode = 0;
            else
                currentNode++;
           
        }
        


    }
    private void Braking()
    {
        if (isBraking)
        {
            carRenderer.material.mainTexture = textureBraking;
            wheelRL.brakeTorque = maxBrakeTorque;
            wheelRR.brakeTorque = maxBrakeTorque;
        }
        else
        {
            carRenderer.material.mainTexture = textureNormal;
            wheelRL.brakeTorque = 0;
            wheelRR.brakeTorque = 0;
        }

    }
}
