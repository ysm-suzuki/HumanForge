using UnityEngine;

using UnityMVC;

namespace GameMain
{
    public class BuildUnitUIModeView : View
    {
        [SerializeField]
        private GameObject _moldsRoot;

        private static string PrefabPath = "Prefabs/GameMain/Board/UI/BuildUnitUIMode";

        new private BuildUnitUIMode _model;

        public static BuildUnitUIModeView Attach(GameObject parent)
        {
            var view = View.Attach<BuildUnitUIModeView>(PrefabPath);
            view.SetParent(parent);
            return view;
        }

        public BuildUnitUIModeView SetModel(BuildUnitUIMode model)
        {
            base.SetModel<BuildUnitUIMode>(model);
            
            _model = model;


            ListUpMolds();

            return this;
        }



        private void ListUpMolds()
        {
            for(int i = 0; i < _model.molds.Count; i++)
            {
                var mold = _model.molds[i];
                var view = UnitMoldView
                            .Attach(_moldsRoot)
                            .SetModel(mold);

                mold.position = Position.Create(
                    0       + (i % 5) * 100,
                    100     + (i / 5) * 120);
            }
        }
    }
}