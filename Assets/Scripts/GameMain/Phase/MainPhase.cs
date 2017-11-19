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
                _gimmickAgent = new GimmickAgent();

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

            
            if (_mapView == null)
                _mapView = MapView
                    .Attach(ViewManager.Instance.GetRoot(GameMainKicker.BoardRootTag))
                    .SetModel(_board.map);


            // kari
            var gimmick1 = new Gimmick(new List<Gimmick.Trigger>
            {
                new Gimmick.Trigger
                {
                    type = Gimmick.Trigger.Type.StartMap
                }
            });
            gimmick1.OnTrrigered += () =>
            {
                var levelData = LevelMasterData.loader.Get(_board.level);
                const int EnemyTeamId = 2; // kari
                foreach (var unit in levelData.enemyUnits)
                {
                    unit.teamId = EnemyTeamId;
                    _board.map.AddUnit(unit);
                }
            };

            var gimmick2 = new Gimmick(new List<Gimmick.Trigger>
            {
                new Gimmick.Trigger
                {
                    type = Gimmick.Trigger.Type.PassTime,
                    value = 10,
                }
            });
            gimmick2.OnTrrigered += () =>
            {
                var enemies = _board.map.enemyUnits;
                foreach (var enemy in enemies)
                    enemy.Attack(new List<FieldObject>
                    {
                        _board.map.playerUnit
                    });
            };

            _gimmickAgent.AddGimmick(gimmick1);
            _gimmickAgent.AddGimmick(gimmick2);


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
