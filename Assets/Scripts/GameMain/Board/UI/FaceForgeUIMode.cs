using System.Collections.Generic;

using UnityMVC;

namespace GameMain
{
    public class FaceForgeUIMode : UIMode
    {
        private int _targetIndex = 0;

        private List<FaceMold> _molds;

        public FaceForgeUIMode()
        {
            ListUpMolds();
            
            var view = FaceForgeUIModeView
                                .Attach(ViewManager.Instance.GetRoot(GameMainKicker.UIRootTag))
                                .SetModel(this);

            OnFinished += () =>
            {
                view.Detach();

                _player.OnManaUpdated -= ManaUpdated;
            };
        }

        override public void SetPlayer(Player player)
        {
            base.SetPlayer(player);

            player.OnManaUpdated += ManaUpdated;
            
            foreach (var mold in _molds)
            {
                mold.RegisterConditionFunction(_player.HasEnoughMana);
                mold.UpdateStatus();
            }
        }

        public FaceForgeUIMode SetTargetIndex(int index)
        {
            _targetIndex = index;
            return this;
        }

        public override void ClickMap(Position position)
        {
            Change(new DefaultUIMode());
        }

        public void Select(FaceMold mold)
        {
            if (!_player.HasEnoughMana(mold.requiringManas))
            {
                return;
            }

            foreach (var pair in mold.requiringManas)
                _player.LoseMana(pair.Key, pair.Value);

            _player.ReplaceFace(
                mold.Pick(),
                _targetIndex);

            Change(new DefaultUIMode());
        }

        

        public void ListUpMolds()
        {
            _molds = new FaceMoldGroup()
                        .Filter(
                            mold => 
                            {
                                return mold.type == FaceData.Type.Normal;
                            })
                        .molds;

            foreach(var mold in _molds)
            {
                mold.OnSelected += () => 
                {
                    Select(mold);
                };
            }
        }


        public List<FaceMold> molds
        {
            get { return _molds; }
        }


        // =============================== delegate
        private void ManaUpdated(Mana mana)
        {
            foreach (var mold in _molds)
                mold.UpdateStatus();
        }
    }
}