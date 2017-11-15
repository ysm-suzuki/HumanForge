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


            ListUpMolds();

            return this;
        }



        private void ListUpMolds()
        {
            for(int i = 0; i < _model.molds.Count; i++)
            {
                var mold = _model.molds[i];
                var view = FaceMoldView
                            .Attach(_moldsRoot)
                            .SetModel(mold);

                mold.position = Position.Create(
                    0       + (i % 5) * 100,
                    100     + (i / 5) * 120);
            }
        }
    }
}