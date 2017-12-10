using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using UnityMVC;

namespace GameMain
{
    public class FaceForgeButtonView : ButtonView
    {
        [SerializeField]
        private Text _kari;

        private static string PrefabPath = "Prefabs/GameMain/Board/UI/FaceForgeButton";
        
        new public static FaceForgeButtonView Attach(GameObject parent)
        {
            var view = View.Attach<FaceForgeButtonView>(PrefabPath);
            view.SetParent(parent);
            return view;
        }

        new public FaceForgeButtonView SetImage(string imageName)
        {
            // debug
            ShowBoundary();
            _kari.text = imageName;

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