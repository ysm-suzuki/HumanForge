using System.Collections.Generic;

using UnityEngine;

using UnityMVC;

namespace GameMain
{
    public class FaceForgeButtonView : ButtonView
    {
        private static string PrefabPath = "Prefabs/GameMain/Board/UI/FaceForgeButton";
        
        public static FaceForgeButtonView Attach(GameObject parent)
        {
            var view = View.Attach<FaceForgeButtonView>(PrefabPath);
            view.SetParent(parent);
            return view;
        }

        public ButtonView SetImage(string imageName)
        {
            // debug
            ShowBoundary();

            return this;
        }
        
        private void ShowBoundary()
        {
            var shapePoints = new List<Position>
            {
                Position.Create(-50,-10),
                Position.Create(50,-10),
                Position.Create(50,10),
                Position.Create(-50,10),
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