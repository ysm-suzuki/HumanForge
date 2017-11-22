using System.Collections.Generic;

using UnityEngine;

using UnityMVC;
using GameMain;

public class MoldViewFactory
{
    public static View View(Model model, GameObject parent)
    {
        if (model is FaceMold)
        {
            var view = FaceMoldView
                .Attach(parent)
                .SetModel(model as FaceMold);
            return view;
        }

        return null;
    }
}