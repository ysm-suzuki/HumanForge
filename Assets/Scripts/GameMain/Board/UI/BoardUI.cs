using UnityMVC;

namespace GameMain
{
    public class BoardUI
    {
        public delegate void EventHandler();
        public event EventHandler OnModeChanged;

        private UIMode _currentUiMode = null;
        
        public void Initialize()
        {
            SetCurrentMode(new DefaultUIMode());
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



        public UIMode mode
        {
            get { return _currentUiMode; }
        }



        private void ModeChange(UIMode uiMode)
        {
            _currentUiMode.End();
            SetCurrentMode(uiMode);
        }

        private void SetCurrentMode(UIMode uiMode)
        {
            uiMode.OnModeChanged += ModeChange;
            _currentUiMode = uiMode;

            if (OnModeChanged != null)
                OnModeChanged();
        }
    }
}