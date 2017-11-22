using UnityEngine;
using UnityEngine.UI;

using UnityMVC;

using GameMain;

public class SimpleMessageNotiifcationView : OneShotEffectView
{
    [SerializeField]
    private Text _message;

    private static string PrefabPath = "Prefabs/GameMain/VisualNotification/SimpleMessageNotiifcation";
    
    public static SimpleMessageNotiifcationView Attach(GameObject parent)
    {
        var view = View.Attach<SimpleMessageNotiifcationView>(PrefabPath);
        view.SetParent(parent);
        return view;
    }

    public SimpleMessageNotiifcationView SetModel(VisualNotification.Product model)
    {
        _message.text = model.text;
        return this;
    }

    public SimpleMessageNotiifcationView RegisterFinishCallback(System.Action callback)
    {
        _finishCallback = () => 
        {
            Detach();
            callback();
        };
        return this;
    }
}
