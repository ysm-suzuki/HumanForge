using UnityMVC;

namespace GameMain
{
    public class DefaultUIMode : UIMode
    {
        public event EventHandler OnFaceUpdated;

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

        public override void SetPlayer(Player player)
        {
            base.SetPlayer(player);

            _player.OnFacesUpdated += () =>
            {
                if (OnFaceUpdated != null)
                    OnFaceUpdated();
            };

            if (OnFaceUpdated != null)
                OnFaceUpdated();
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

        public void ClickFaceForgeButton(int index)
        {
            Change(new FaceForgeUIMode()
                        .SetTargetIndex(index));
        }


        public int faceCount
        {
            get { return _player != null
                        ? _player.faceCount
                        : 0; }
        }
    }
}