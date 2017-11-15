using System.Collections.Generic;

using UnityEngine;

using UnityMVC;

namespace GameMain
{
    public class FaceMoldView : View
    {
        private static string PrefabPath = "Prefabs/GameMain/Board/Player/FaceMold";

        new private FaceMold _model;

        public static FaceMoldView Attach(GameObject parent)
        {
            var view = View.Attach<FaceMoldView>(PrefabPath);
            view.SetParent(parent);
            return view;
        }

        public FaceMoldView SetModel(FaceMold model)
        {
            base.SetModel<FaceMold>(model);

            _model = model;

            _controller.OnClicked += () => 
            {
                _model.Select();
            };


            // debug
            ShowBoundary();

            return this;
        }



        private void ShowBoundary()
        {
            var shapePoints = new List<Position>
            {
                Position.Create(-50,-50),
                Position.Create(50,-50),
                Position.Create(50,50),
                Position.Create(-50,50),
            };

            int vertexCount = shapePoints.Count;
            for (int i = 0; i < vertexCount; i++)
            {
                var current = shapePoints[i % vertexCount];
                var next = shapePoints[(i + 1) % vertexCount];

                LineSegmentView
                    .Attach(GetRoot())
                    .SetSegment(current.ToVector2(), next.ToVector2())
                    .SetColor(UnityEngine.Color.yellow);
            }
        }
    }
}