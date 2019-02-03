/*Written by Mor Halfon 
 * 
 * This script takes all the children paths of all the different junctions in to a Transform array.
 * The method chooseRandomDirection is activated when a vehicle arrives an intersection.
 * This method chooses the random direction on which the car will follow.
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunctionEditor : MonoBehaviour
{
    public List<Transform> junctions = new List<Transform>();
    Transform[] transforms;

    // Start is called before the first frame update
    void Start()
    {
        transforms = GetComponentsInChildren<Transform>();
        junctions.Clear();
        foreach (Transform path in transforms)
        {
            if (path != this.transform && path.parent == transform)
            {
                junctions.Add(path);
            }
        }
    }

    public Transform chooseRandomDirection(EditorPath currentPath, Transform lastNode)
    {
        int currDirection;
        List<Transform> direction_options = new List<Transform>();
        for (currDirection = 0; currDirection < junctions.Count; currDirection++)
        {
            if (junctions[currDirection].name.Equals(currentPath.name))
            {
                Debug.Log("path is: " + currentPath.name);
            }
            if (!junctions[currDirection].name.Equals(currentPath.name))
            {
                Transform currNode = junctions[currDirection].GetChild(0);
                if (currNode.name == lastNode.name)
                {
                    direction_options.Add(junctions[currDirection]);
                }
            }
        }
        if (direction_options.Count == 0)
        {
            return null;
        }
        int rand = Random.Range(0, direction_options.Count);
        return direction_options[rand];
    }
}
