using System.Collections.Generic;

using UnityEngine;

using UnityMVC;

public class MoldListView : View
{
    [SerializeField]
    protected GameObject _listRoot;

    private static readonly string PrefabPath = "Prefabs/Common/UI/MoldList";


    protected Position OriginalPosition = new Position(80, -50);
    protected float HorizontalInterval = 110;
    protected float VerticalInterval = 110;
    protected float HorizontalCount = 5;

    protected List<Model> _molds = new List<Model>();


    
    public static MoldListView Attach(GameObject parent)
    {
        var view = View.Attach<MoldListView>(PrefabPath);
        Debug.Assert(view != null, "prefab : " + PrefabPath + " has no MoldListView.");
        view.SetParent(parent);
        return view;
    }

    virtual public void Initialize<T>(List<T> molds) where T : Model
    {
        AddMolds(molds);
    }

    private void AddMolds<T>(List<T> molds) where T : Model
    {
        foreach (var mold in molds)
            AddMold(mold);
    }

    private void AddMold<T>(T mold) where T : Model
    {
        var view = MoldViewFactory.View(mold, _listRoot);

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