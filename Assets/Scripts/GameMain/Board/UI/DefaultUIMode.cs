using UnityMVC;

namespace GameMain
{
    public class DefaultUIMode : UIMode
    {
        public DefaultUIMode()
        {
            var view = DefaultUIModeView
                                .Attach(ViewManager.Instance.GetRoot(GameMainKicker.UIRootTag))
                                .SetModel(this);

            OnFinished += () =>
            {
                view.Detach();
            };
        }

        public override void ClickMap(Position position)
        {
            _map.playerUnit.MoveTo(position);
        }

        override public void ClickUnit(Unit unit)
        {
            if (unit.isOwnedUnit)
                Change(new OwnedUnitUIMode()
                            .SetUnit(unit));
        }

        public void ClickFaceForgeButton()
        {
            Change(new FaceForgeUIMode());
        }
    }
}