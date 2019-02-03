/*Written by Mor Halfon 
 * 
 * This script takes all the children paths into a Transform array.
 * The method findPath finds the pth to continue from a junction.
 * the paths in this script are only linking paths between junctions.
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathsEditor : MonoBehaviour
{
    public List<Transform> paths = new List<Transform>();
    Transform[] transforms;

    // Start is called before the first frame update
    void Start()
    {
        transforms = GetComponentsInChildren<Transform>();
        paths.Clear();
        foreach(Transform path in transforms)
        {
            if(path != this.transform && path.parent == this.transform)
            {
                paths.Add(path); 
            }
        }
    }

    public Transform findPath(EditorPath prevPath, Transform lastNode)
    {
        int currPath;
        for (currPath = 0; currPath < paths.Count; currPath++)
        {
            if (paths[currPath].name.Equals(prevPath.name))
            {
                Debug.Log("path is: " + prevPath.name);

            }
            if (!paths[currPath].name.Equals(prevPath.name))
            {
                Transform currNode = paths[currPath].GetChild(0);
                if (currNode.name == lastNode.name)
                {
                    return paths[currPath];
                }
            }
        }
        return null;
    }
   
}
