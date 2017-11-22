using UnityEngine;

using UnityMVC;

namespace GameMain
{
    public class FaceForgeUIModeView : View
    {
        [SerializeField]
        private GameObject _moldsRoot;

        private static string PrefabPath = "Prefabs/GameMain/Board/UI/FaceForgeUIMode";

        new private FaceForgeUIMode _model;

        public static FaceForgeUIModeView Attach(GameObject parent)
        {
            var view = View.Attach<FaceForgeUIModeView>(PrefabPath);
            view.SetParent(parent);
            return view;
        }

        public FaceForgeUIModeView SetModel(FaceForgeUIMode model)
        {
            base.SetModel<FaceForgeUIMode>(model);
            
            _model = model;
            
            FaceMoldListView
                .Attach(_moldsRoot)
                .Initialize(_model.molds);

            return this;
        }
    }
}