using System.Collections.Generic;

using UnityEngine;

using UnityMVC;

namespace GameMain
{
    public class ButtonView : View
    {
        private static string PrefabPath = "Prefabs/GameMain/Board/UI/Button";
        
        public static ButtonView Attach(GameObject parent)
        {
            var view = View.Attach<ButtonView>(PrefabPath);
            view.SetParent(parent);
            return view;
        }

        public ButtonView RegisterOnClickCallback(System.Action callback)
        {
            _controller.OnClicked += () =>
            {
                callback();
            };

            return this;
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