using UnityEngine;

using UnityMVC;

namespace GameMain
{
    public class UnitView : View
    {
        private static string PrefabPath = "Prefabs/GameMain/Board/Unit";
        
        public static UnitView Attach(GameObject parent)
        {
            var view = View.Attach<UnitView>(PrefabPath);
            view.SetParent(parent);
            return view;
        }

        public UnitView SetModel(Unit model)
        {
            base.SetModel<Unit>(model);



            return this;
        }
    }
}