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
                int childCount = _faceForgeButtonRoot.transform.childCount;
                for (int i = 0; i < childCount; i++)
                    GameObject.Destroy(_faceForgeButtonRoot.transform.GetChild(i).gameObject);

                for (int i = 0; i < _model.faceCount; i++)
                {
                    int index = i;
                    var view = FaceForgeButtonView
                        .Attach(_faceForgeButtonRoot)
                        .SetImage(_model.GetFaceName(index)) // kari
                        .RegisterOnClickCallback(() =>
                        {
                            _model.ClickFaceForgeButton(index);
                        });

                    // kari
                    view.transform.localPosition = Position.Create(
                        -250 + (i % 6) * 100,
                        -50 + (i / 6) * 120)
                        .ToVector3();
                }
            };
            

            return this;
        }
    }
}