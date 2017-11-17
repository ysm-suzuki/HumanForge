using System.Collections.Generic;

using UnityMVC;

namespace GameMain
{
    public class BuildUnitUIMode : UIMode
    {
        private List<UnitMold> _molds;

        public BuildUnitUIMode()
        {
            ListUpMolds();


            var view = BuildUnitUIModeView
                                .Attach(ViewManager.Instance.GetRoot(GameMainKicker.UIRootTag))
                                .SetModel(this);

            OnFinished += () =>
            {
                view.Detach();
            };
        }
        
        public override void ClickMap(Position position)
        {
            Change(new DefaultUIMode());
        }

        public void Select(UnitMold mold)
        {
            if (!_player.HasEnoughMana(mold.requiringManas))
            {
                return;
            }

            foreach (var pair in mold.requiringManas)
                _player.LoseMana(pair.Key, pair.Value);

            var unit = mold.Pick();
            unit.position = _player.position;

            _player.MoveTo(unit.position + Position.Create(0, -2 * unit.sizeRadius));
            _player.PlaceUnit(unit);

            Change(new DefaultUIMode());
        }

        

        public void ListUpMolds()
        {
            _molds = new UnitMoldGroup()
                            .Filter(molds => 
                            {
                                // kari
                                return true;
                            })
                            .molds;

            foreach (var mold in _molds)
            {
                mold.OnSelected += () => 
                {
                    Select(mold);
                };
            }
        }


        public List<UnitMold> molds
        {
            get { return _molds; }
        }
    }
}