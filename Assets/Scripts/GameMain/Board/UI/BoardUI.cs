using UnityMVC;

namespace GameMain
{
    public class BoardUI
    {
        private UIMode _currentUiMode = null;

        private Map _map = null;
        
        public void Initialize(Map map)
        {
            _map = map;

            _currentUiMode = new DefaultUIMode();
            _currentUiMode.OnModeChanged += OnModeChanged;
            _currentUiMode.SetMap(_map);
        }

        public void ClickMap()
        {
            _currentUiMode.ClickMap();
        }
        public void ClickMap(Position position)
        {
            _currentUiMode.ClickMap(position);
        }

        public void ClickUnit(Unit unit)
        {
            _currentUiMode.ClickUnit(unit);
        }

        private void OnModeChanged(UIMode uiMode)
        {
            uiMode.OnModeChanged += OnModeChanged;
            uiMode.SetMap(_map);

            _currentUiMode.End();
            _currentUiMode = uiMode;
        }
    }
}