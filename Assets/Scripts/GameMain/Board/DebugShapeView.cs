using UnityEngine;

using UnityMVC;

namespace GameMain
{
    public class DebugShapeView : View
    {
        private static string PrefabPath = "Prefabs/GameMain/Board/DebugShape";


        new private FieldObject _model;


        public static DebugShapeView Attach(GameObject parent)
        {
            var view = View.Attach<DebugShapeView>(PrefabPath);
            view.SetParent(parent);
            return view;
        }

        private void OnDrawGizmos()
        {
            for (int i = 0; i < _model.shapePoints.Count; i++)
            {
                int current = i % _model.shapePoints.Count;
                int next = (i + 1) % _model.shapePoints.Count;

                var from = _model.position.ToVector3() + _model.shapePoints[current].ToVector3();
                var to = _model.position.ToVector3() + _model.shapePoints[next].ToVector3();

                Gizmos.DrawLine(from / 100, to / 100);

                Debug.Log("from : " + from);
                Debug.Log("to : " + to);
            }
        }

        public DebugShapeView SetModel(FieldObject model)
        {
            base.SetModel<FieldObject>(model);

            _model = model;

            return this;
        }
    }
}