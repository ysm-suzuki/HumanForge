using System.Collections.Generic;

using UnityEngine;

public class ViewManager
{
    public static ViewManager _instance = null;
    public static ViewManager Instance
    {
        get
        {
            return _instance ?? (_instance = new ViewManager());
        }
    }
    private ViewManager() { }
    public void TearDown()
    {
        _instance = null;
    }


    private Dictionary<string, GameObject> _roots = new Dictionary<string, GameObject>();

    public void RegisterRoot(GameObject root, string tag = "default")
    {
        Debug.Assert(root != null, "root is null.");

        _roots[tag] = root;
    }

    public GameObject GetRoot(string tag = "default")
    {
        Debug.Assert(_roots.ContainsKey(tag), "The root not found for tag : " + tag);

        return _roots[tag];
    }
}
