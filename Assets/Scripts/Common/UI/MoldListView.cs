using System.Collections.Generic;

using UnityEngine;

using UnityMVC;

public class MoldListView : View
{
    [SerializeField]
    private GameObject _listRoot;

    private static readonly string PrefabPath = "Prefabs/Common/UI/MoldList";


    private const float HorizontalInterval = 110;
    private const float VerticalInterval = 110;
    private const float HorizontalCount = 5;

    private List<Mold> _molds = new List<Mold>();

    private Position OriginalPosition = new Position(80, -50);

    
    public static MoldListView Attach(GameObject parent)
    {
        var view = View.Attach<MoldListView>(PrefabPath);
        Debug.Assert(view != null, "prefab : " + PrefabPath + " has no MoldListView.");
        view.SetParent(parent);
        return view;
    }
    
    public void AddMolds(List<Mold> molds)
    {
        foreach (var mold in molds)
            AddMold(mold);
    }

    public void AddMold(Mold mold)
    {
        var view = MoldView
                    .Attach(_listRoot)
                    .SetModel(mold);

        int index = _molds.Count;

        var position = new Position
            (
                (index % HorizontalCount) * HorizontalInterval,
                -1 * Mathf.FloorToInt(index / HorizontalCount) * VerticalInterval
            ) +
            OriginalPosition;
        view.transform.localPosition = position.ToVector3();

        _molds.Add(mold);
    }
}