using UnityEngine;

using UnityMVC;

namespace GameMain
{
    public class MapView : View
    {
        [SerializeField]
        private GameObject _fieldObjectsRoot;

        private static string PrefabPath = "Prefabs/GameMain/Board/Map";

        new private Map _model;

        public static MapView Attach(GameObject parent)
        {
            var view = View.Attach<MapView>(PrefabPath);
            view.SetParent(parent);
            return view;
        }

        public MapView SetModel(Map model)
        {
            base.SetModel<Map>(model);

            _model = model;

            model.OnUnitAdded += unit =>
            {
                UnitView
                    .Attach(_fieldObjectsRoot)
                    .SetModel(unit);
            };

            model.OnWallAdded += wall =>
            {
                WallView
                    .Attach(_fieldObjectsRoot)
                    .SetModel(wall);
            };

            UpdatePosition();

            return this;
        }
    }
}