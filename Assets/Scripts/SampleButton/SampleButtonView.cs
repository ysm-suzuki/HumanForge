using UnityEngine;
using UnityMVC;

public class SampleButtonView : View
{
    public const string PrefabPath = "SampleButton/SampleButton";

    public static SampleButtonView Attach(GameObject parent)
    {
        var view = View.Attach<SampleButtonView>(PrefabPath);
        view.SetParent(parent);
        return view;
    }

    public SampleButtonView SetModel(SampleButton model)
    {
        base.SetModel<SampleButton>(model);

        _controller.OnClicked += () => 
        {
            model.Click();
        };

        return this;
    }

    // called by Unity
    void Start()
    {
        PlayAnimation("SampleButton");
    }
}