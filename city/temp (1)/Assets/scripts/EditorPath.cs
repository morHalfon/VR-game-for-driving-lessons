using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorPath : MonoBehaviour {

    public Color lineColor = Color.blue;
    public List<Transform> path_objs = new List<Transform>();
    Transform[] transforms;

    private void OnDrawGizmos()
    {
        Gizmos.color = lineColor;
        transforms = GetComponentsInChildren<Transform>();
        path_objs.Clear();
        foreach(Transform path_obj in transforms)
        {
            if(path_obj != this.transform)
            {
                path_objs.Add(path_obj);
            }
        }
        int currNode;
        Vector3 currentNode;
        Vector3 previousNode;
        Gizmos.DrawSphere(path_objs[0].position, 0.3f);
        for (currNode = 0; currNode < path_objs.Count; currNode++)
        {
            currentNode = path_objs[currNode].position;
            if (currNode > 0)
            {
                previousNode = path_objs[currNode - 1].position;
                Gizmos.DrawLine(previousNode, currentNode);
                Gizmos.DrawSphere(currentNode, 0.3f);
            }
        }
    }
}
