﻿using System.Collections.Generic;

using UnityMVC;

namespace GameMain
{
    public class MainPhase : Phase
    {
        public static string Tag = "MainPhase";

        private Board _board = null;
        private GimmickAgent _gimmickAgent = null;
        private VisualNotificationAgent _visualNotificationAgent = null;

        private MapView _mapView = null;

        override public void Initialize()
        {
            if (_board == null)
                _board = new Board();
            if (_gimmickAgent == null)
                _gimmickAgent = new GimmickAgent(_board);
            if (_visualNotificationAgent == null)
                _visualNotificationAgent = new VisualNotificationAgent(_board.level);

            _board.OnMapStarted += () =>
            {
                _gimmickAgent.Notify(new Gimmick.Trigger
                {
                    type = Gimmick.Trigger.Type.StartMap,
                });
                _visualNotificationAgent.Notify(new VisualNotification.Trigger
                {
                    type = VisualNotification.Trigger.Type.StartMap,
                });
            };
            _board.OnTimeTicked += seconds =>
            {
                _gimmickAgent.Notify(new Gimmick.Trigger
                {
                    type = Gimmick.Trigger.Type.PassTime,
                    value = seconds
                });
                _visualNotificationAgent.Notify(new VisualNotification.Trigger
                {
                    type = VisualNotification.Trigger.Type.PassTime,
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
            if (_visualNotificationAgent.lockPhase)
                return;

            _board.Tick(delta);
        }

        override protected void End()
        {
            base.End(FinishPhase.Tag);
        }


        private void Win()
        {
            SimpleMessageNotiifcationView
                .Attach(ViewManager.Instance.GetRoot(GameMainKicker.InformationRootTag))
                .SetModel(new VisualNotification.Product
                {
                    type = VisualNotification.Product.Type.ShowMessage,
                    text = "クリア！"
                });

            End();
        }

        private void Lose()
        {
            SimpleMessageNotiifcationView
                .Attach(ViewManager.Instance.GetRoot(GameMainKicker.InformationRootTag))
                .SetModel(new VisualNotification.Product
                {
                    type = VisualNotification.Product.Type.ShowMessage,
                    text = "Game Over"
                });

            End();
        }
    }
}
