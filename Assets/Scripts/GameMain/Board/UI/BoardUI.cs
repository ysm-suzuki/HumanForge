namespace GameMain
{
    public class BoardUI
    {
        private UIMode _uiMode = new DefaultUIMode();

        public BoardUI()
        {
            _uiMode.OnModeChanged += OnModeChanged;
        }

        public void ClickMap()
        {
            _uiMode.ClickMap();
        }

        public void ClickUnit(Unit unit)
        {
            _uiMode.ClickUnit(unit);
        }

        private void OnModeChanged(UIMode uiMode)
        {
            uiMode.OnModeChanged += OnModeChanged;

            _uiMode.End();
            _uiMode = uiMode;
        }
    }
}