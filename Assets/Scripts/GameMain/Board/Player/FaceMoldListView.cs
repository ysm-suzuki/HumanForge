using System.Collections.Generic;

using UnityEngine;

using UnityMVC;

public class FaceMoldListView : MoldListView
{
    private static readonly string PrefabPath = "Prefabs/GameMain/Board/Player/FaceMoldList";
    
    new public static FaceMoldListView Attach(GameObject parent)
    {
        var view = View.Attach<FaceMoldListView>(PrefabPath);
        Debug.Assert(view != null, "prefab : " + PrefabPath + " has no FaceMoldListView.");
        view.SetParent(parent);
        return view;
    }

    override public void Initialize<T>(List<T> molds)
    {
        OriginalPosition = new Position(50, -50);
        HorizontalInterval = 120;
        VerticalInterval = 120;
        HorizontalCount = 5;

        base.Initialize(molds);
    }
}