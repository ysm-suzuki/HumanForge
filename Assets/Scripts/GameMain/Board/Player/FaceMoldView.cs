using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using UnityMVC;

namespace GameMain
{
    public class FaceMoldView : View
    {
        [SerializeField]
        private Text _name;
        [SerializeField]
        private Text _description;
        
        [SerializeField]
        private GameObject _black;
        [SerializeField]
        private Text _message;

        [SerializeField]
        private Text _requiringRedMana;
        [SerializeField]
        private Text _requiringGreenMana;
        [SerializeField]
        private Text _requiringBlueMana;

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

            _model.OnStatusUpdated += () => 
            {
                UpdateStatus();
            };

            _controller.OnClicked += () => 
            {
                _model.Select();
            };


            // debug
            ShowBoundary();
            _name.text = _model.name;
            _description.text = _model.description;

            _requiringRedMana.text = "赤:" + _model.requiringManas[ManaData.Type.Red].ToString();
            _requiringGreenMana.text = "緑:" + _model.requiringManas[ManaData.Type.Green].ToString();
            _requiringBlueMana.text = "青:" + _model.requiringManas[ManaData.Type.Blue].ToString();

            return this;
        }
        
        private void UpdateStatus()
        {
            _black.SetActive(!_model.isAvailable);
            _message.text = _model.isAvailable
                ? ""
                : "マナが足りません";
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