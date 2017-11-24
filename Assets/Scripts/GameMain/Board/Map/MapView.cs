using UnityEngine;

using UnityMVC;

namespace GameMain
{
    public class MapView : View
    {
        [SerializeField]
        private GameObject _fieldObjectsRoot;

        private static string PrefabPath = "Prefabs/GameMain/Board/Map";

        new private Map _model;

        public static MapView Attach(GameObject parent)
        {
            var view = View.Attach<MapView>(PrefabPath);
            view.SetParent(parent);
            return view;
        }

        public MapView SetModel(Map model)
        {
            base.SetModel<Map>(model);

            _model = model;

            model.OnUnitAdded += unit =>
            {
                UnitView
                    .Attach(_fieldObjectsRoot)
                    .SetModel(unit);
            };

            model.OnWallAdded += wall =>
            {
                WallView
                    .Attach(_fieldObjectsRoot)
                    .SetModel(wall);
            };

            model.OnBarricadeAdded += barricade => 
            {
                BarricadeView
                    .Attach(_fieldObjectsRoot)
                    .SetModel(barricade);
            };

            SetUpController();

            UpdatePosition();

            return this;
        }


        private void SetUpController()
        {
            _controller.OnClicked += () => 
            {
                _model.ui.ClickMap();
            };

            _controller.OnTouchFinished += position =>
            {
                var clickdPosition = Position.Create(position.x, position.y);

                var resolution = ResolutionManager.Instance;

                clickdPosition -= Position.Create(
                                    resolution.width / 2,
                                    resolution.height / 2);

                clickdPosition -= _model.position;

                _model.ui.ClickMap(clickdPosition);
            };

            _controller.OnTouchMoved += (diff, duration) => 
            {
                var newPosition = _model.position + Position.Create(diff.x, diff.y);

                float resW = ResolutionManager.Instance.width;
                float resH = ResolutionManager.Instance.height;

                float minX = -1 * _model.width / 2 + resW / 2;
                float maxX = _model.width / 2 - resW / 2;
                float minY = -1 * _model.height / 2 + resH / 2;
                float maxY = _model.height / 2 - resH / 2;

                if (newPosition.x < minX)
                    newPosition.x = minX;
                if (newPosition.x > maxX)
                    newPosition.x = maxX;
                if (newPosition.y < minY)
                    newPosition.y = minY;
                if (newPosition.y > maxY)
                    newPosition.y = maxY;

                _model.position = newPosition;
            };
        }
    }
}