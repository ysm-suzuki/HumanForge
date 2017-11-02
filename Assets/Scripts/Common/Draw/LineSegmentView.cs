using UnityEngine;
using UnityEngine.UI;

using UnityMVC;

namespace GameMain
{
    public class LineSegmentView : View
    {
        [SerializeField]
        private Image _image;
        [SerializeField]
        private RectTransform _imageRectTransform;

        private static string PrefabPath = "Prefabs/Common/Draw/LineSegment";
        
        public static LineSegmentView Attach(GameObject parent)
        {
            var view = View.Attach<LineSegmentView>(PrefabPath);
            view.SetParent(parent);
            return view;
        }

        public LineSegmentView SetSegment(Vector2 from, Vector2 to, float width = 4)
        {
            var length = (to - from).magnitude;
            var radian = Mathf.Atan2((to - from).y, (to - from).x);
            var theta = 180 * radian / Mathf.PI - 90;

            _imageRectTransform.anchoredPosition = from;
            _imageRectTransform.sizeDelta = new Vector2(width, length);
            _imageRectTransform.localRotation = Quaternion.Euler(0, 0, theta);

            return this;
        }

        public LineSegmentView SetColor(Color color)
        {
            _image.color = color;

            return this;
        }
    }
}