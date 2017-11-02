using UnityEngine;

using UnityMVC;

namespace GameMain
{
    public class WallView : View
    {
        private static string PrefabPath = "Prefabs/GameMain/Board/Wall";

        new private Wall _model;

        public static WallView Attach(GameObject parent)
        {
            var view = View.Attach<WallView>(PrefabPath);
            view.SetParent(parent);
            return view;
        }

        public WallView SetModel(Wall model)
        {
            base.SetModel<Wall>(model);

            _model = model;

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