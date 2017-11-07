using UnityEngine;

using UnityMVC;

namespace GameMain
{
    public class OwnedUnitUIModeView : View
    {
        private static string PrefabPath = "Prefabs/GameMain/Board/UI/OwnedUnitUIMode";

        new private OwnedUnitUIMode _model;

        public static OwnedUnitUIModeView Attach(GameObject parent)
        {
            var view = View.Attach<OwnedUnitUIModeView>(PrefabPath);
            view.SetParent(parent);
            return view;
        }

        public OwnedUnitUIModeView SetModel(OwnedUnitUIMode model)
        {
            base.SetModel<OwnedUnitUIMode>(model);
            
            _model = model;
            

            return this;
        }


        private void SetUpController()
        {

        }
    }
}