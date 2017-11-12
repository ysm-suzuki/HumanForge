using UnityEngine;

using UnityMVC;

namespace GameMain
{
    public class BarricadeView : View
    {
        private static string PrefabPath = "Prefabs/GameMain/Board/Barricade";

        new private Barricade _model;

        public static BarricadeView Attach(GameObject parent)
        {
            var view = View.Attach<BarricadeView>(PrefabPath);
            view.SetParent(parent);
            return view;
        }

        public BarricadeView SetModel(Barricade model)
        {
            base.SetModel<Barricade>(model);

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
                    .SetColor(UnityEngine.Color.white);
            }
        }
    }
}