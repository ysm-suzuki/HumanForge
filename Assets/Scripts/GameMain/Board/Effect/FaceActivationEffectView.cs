using System.Collections.Generic;

using UnityEngine;

using UnityMVC;

namespace GameMain
{
    public class FaceActivationEffectView : OneShotEffectView
    {
        private static string PrefabPath = "Prefabs/GameMain/Board/Effect/FaceActivationEffect";
        
        public static FaceActivationEffectView Attach(GameObject parent)
        {
            var view = View.Attach<FaceActivationEffectView>(PrefabPath);
            view.SetParent(parent);
            return view;
        }

        public void RegisterFinishCallback(System.Action callback)
        {
            _finishCallback = callback;
        }
    }
}