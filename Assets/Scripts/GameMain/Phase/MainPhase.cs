using System.Collections.Generic;

using UnityMVC;

namespace GameMain
{
    public class MainPhase : Phase
    {
        public static string Tag = "MainPhase";

        private Board _board = null;
        private GimmickAgent _gimmickAgent = null;

        private MapView _mapView = null;

        override public void Initialize()
        {
            if (_board == null)
                _board = new Board();
            if (_gimmickAgent == null)
                _gimmickAgent = new GimmickAgent(_board);

            _board.OnMapStarted += () =>
            {
                _gimmickAgent.Notify(new Gimmick.Trigger
                {
                    type = Gimmick.Trigger.Type.StartMap,
                });
            };
            _board.OnTimeTicked += seconds => 
            {
                _gimmickAgent.Notify(new Gimmick.Trigger
                {
                    type = Gimmick.Trigger.Type.PassTime,
                    value = seconds
                });
            };
            _board.OnBossWipedOut += () => 
            {
                Win();
            };
            _board.OnPlayerDead += () => 
            {
                Lose();
            };

            _gimmickAgent.SetLevel(_board.level);



            if (_mapView == null)
                _mapView = MapView
                    .Attach(ViewManager.Instance.GetRoot(GameMainKicker.BoardRootTag))
                    .SetModel(_board.map);

            _board.Start();
        }

        override public void Tick(float delta)
        {
            _board.Tick(delta);
        }

        override protected void End()
        {
            base.End(FinishPhase.Tag);
        }


        private void Win()
        {
            UnityEngine.Debug.Log("Win");
            End();
        }

        private void Lose()
        {
            UnityEngine.Debug.Log("Lose");
            End();
        }
    }
}
