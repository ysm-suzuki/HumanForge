using UnityEngine;

using UnityMVC;

namespace GameMain
{
    public class UnitView : View
    {
        private static string PrefabPath = "Prefabs/GameMain/Board/Unit";

        new private Unit _model;

        public static UnitView Attach(GameObject parent)
        {
            var view = View.Attach<UnitView>(PrefabPath);
            view.SetParent(parent);
            return view;
        }

        public UnitView SetModel(Unit model)
        {
            base.SetModel<Unit>(model);

            _model = model;

            model.OnRemoved += () => { Detach(); };

            UpdatePosition();

            // debug
            ShowBoundary();

            return this;
        }


        private void ShowBoundary()
        {
            int vertexCount = _model.shapePoints.Count;
            for (int i = 0; i < vertexCount; i++)
            {
                var current = _model.shapePoints[i % vertexCount];
                var next = _model.shapePoints[(i + 1) % vertexCount];

                LineSegmentView
                    .Attach(GetRoot())
                    .SetSegment(current.ToVector2(), next.ToVector2())
                    .SetColor(UnityEngine.Color.blue);
            }
        }
    }
}