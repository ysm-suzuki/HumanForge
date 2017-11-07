namespace GameMain
{
    public class OwnedUnitUIMode : UIMode
    {
        private Unit _unit;
        
        public OwnedUnitUIMode()
        {
            var view = OwnedUnitUIModeView
                                .Attach(ViewManager.Instance.GetRoot(GameMainKicker.UIRootTag))
                                .SetModel(this);

            OnFinished += () =>
            {
                view.Detach();
            };
        }

        public OwnedUnitUIMode SetUnit(Unit unit)
        {
            _unit = unit;
            return this;
        }


        override public void ClickMap()
        {
            Change(new DefaultUIMode());
        }

        override public void ClickUnit(Unit unit)
        {

        }
    }
}