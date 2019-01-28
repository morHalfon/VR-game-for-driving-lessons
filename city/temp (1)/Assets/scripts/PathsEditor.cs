/*Written by Mor Halfon 
 * 
 * This script takes all the children paths into a Transform array.
 * The method chooseRandomPath is choosing a random path for the car to foolow
 * when raching a crossroad.
 * the path is chosen from an array of continuous paths.
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
            if(path != this.transform && path.parent == transform)
            {
                paths.Add(path); 
            }
        }
    }

    public Transform chooseRandomPath(EditorPath path, Transform node)
    {
        int currPath;
        string node_name = node.name;
        List<Transform> path_options = new List<Transform>();
        for (currPath = 0; currPath < paths.Count; currPath++)
        {
            if(paths[currPath].name.Equals(path.name))
            {
                Debug.Log("path is: " + path.name);

            }
            if(!paths[currPath].name.Equals(path.name))
            {
                Transform currNode = paths[currPath].GetChild(0);
                if(currNode.name == node.name)
                {
                    path_options.Add(paths[currPath]);
                }
            }
        }
        if(path_options.Count == 0)
        {
            return null;
        }
        int rand = Random.Range(0, path_options.Count);
        return path_options[rand];
    }
    
}
