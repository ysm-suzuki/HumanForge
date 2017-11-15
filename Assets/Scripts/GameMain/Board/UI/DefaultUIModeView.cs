using UnityEngine;

using UnityMVC;

namespace GameMain
{
    public class DefaultUIModeView : View
    {
        [SerializeField]
        private GameObject _faceForgeButtonRoot;

        private static string PrefabPath = "Prefabs/GameMain/Board/UI/DefaultUIMode";

        new private DefaultUIMode _model;

        public static DefaultUIModeView Attach(GameObject parent)
        {
            var view = View.Attach<DefaultUIModeView>(PrefabPath);
            view.SetParent(parent);
            return view;
        }

        public DefaultUIModeView SetModel(DefaultUIMode model)
        {
            base.SetModel<DefaultUIMode>(model);
            
            _model = model;


            ButtonView
                .Attach(_faceForgeButtonRoot)
                .SetImage("kari")
                .RegisterOnClickCallback(()=> 
                {
                    _model.ClickFaceForgeButton();
                });


            return this;
        }
    }
}