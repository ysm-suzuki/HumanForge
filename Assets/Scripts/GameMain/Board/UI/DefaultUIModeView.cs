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

            _model.OnFaceUpdated += () =>
            {
                // kari
                for (int i = 0; i < _model.faceCount; i++)
                {
                    int index = i;
                    var view = FaceForgeButtonView
                        .Attach(_faceForgeButtonRoot)
                        .SetImage("kari")
                        .RegisterOnClickCallback(() =>
                        {
                            _model.ClickFaceForgeButton(index);
                        });

                    view.transform.localPosition = Position.Create(
                        -250 + (i % 5) * 100,
                        -50 + (i / 5) * 120)
                        .ToVector3();
                }
            };
            

            return this;
        }
    }
}