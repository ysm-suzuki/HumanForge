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
            };
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
            // kari
            _molds = new List<FaceMold>
            {
                new FaceMold(
                    new FaceData
                    {
                        manaGenerators = new Dictionary<ManaData.Type, float>
                        {
                            { ManaData.Type.Red, 2.0f},
                        }
                    },
                    new Dictionary<ManaData.Type, float>
                    {
                        { ManaData.Type.Red, 3.0f},
                        { ManaData.Type.Green, 0.0f},
                        { ManaData.Type.Blue, 0.0f},
                    }),
            };


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
    }
}