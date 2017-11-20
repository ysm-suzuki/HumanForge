using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using UnityMVC;

public class MoldView : View
{
    [SerializeField]
    private Image _iconImage;

    private static readonly string PrefabPath = "Prefabs/Common/UI/Mold";

    new protected Mold _model = null;

    public static MoldView Attach(GameObject parent)
    {
        var view = View.Attach<MoldView>(PrefabPath);
        Debug.Assert(view != null, "prefab : " + PrefabPath + " has no MoldView.");
        view.SetParent(parent);
        return view;
    }
    

    public MoldView SetModel(Mold model)
    {
        _model = model;

        _controller.OnClicked += () => 
        {
            model.Select();
        };

        return this;
    }

    void Start()
    {
        SetUpIconImage();
    }

    private void SetUpIconImage()
    {
    }
}