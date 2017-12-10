﻿using UnityMVC;

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
                _player.OnFacesUpdated -= FaceUpdated;
            };
        }



        public override void SetPlayer(Player player)
        {
            base.SetPlayer(player);

            _player.OnFacesUpdated += FaceUpdated;
            
            if (OnFaceUpdated != null)
                OnFaceUpdated();
        }

        public override void ClickMap(Position position)
        {
            _map.playerUnit.MoveTo(position);
        }

        override public void ClickUnit(Unit unit)
        {
            if (unit.isPlayerUnit)
                Change(new BuildUnitUIMode());
            else if (unit.isOwnedUnit)
                Change(new OwnedUnitUIMode()
                            .SetUnit(unit));
        }

        public void ClickFaceForgeButton(int index)
        {
            Change(new FaceForgeUIMode()
                        .SetTargetIndex(index));
        }

        public void ClickBuildUnitButton()
        {
            Change(new BuildUnitUIMode());
        }

        public int faceCount
        {
            get { return _player != null
                        ? _player.faceCount
                        : 0; }
        }

        public string GetFaceName(int index)
        {
            return _player.GetFaceName(index);
        }

        // =============================== delegate
        private void FaceUpdated()
        {
            if (OnFaceUpdated != null)
                OnFaceUpdated();
        }
    }
}