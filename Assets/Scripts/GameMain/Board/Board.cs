using System.Collections.Generic;

using UnityMVC;

namespace GameMain
{
    public class Board
    {
        public delegate void EventHandler();
        public event EventHandler OnBossWipedOut;
        public event EventHandler OnPlayerDead;

        private Map _map = new Map();
        private BoardUI _ui = new BoardUI();

        private Player _player = null;

        private int _level = 1;

        public Board()
        {
            _level = 1; // kari

            SetUpUI();
            SetUpMap();
            SetUpPlayer();
        }

        public void Tick(float delta)
        {
            _map.Tick(delta);

            if (_player != null)
                _player.Tick(delta);
        }
        

        public Map map
        {
            get { return _map; }
        }



        private void SetUpUI()
        {
            _ui.Initialize();
            _ui.mode.SetMap(_map);
            _ui.OnModeChanged += () =>
            {
                _ui.mode.SetMap(_map);
                _ui.mode.SetPlayer(_player);
            };
        }

        private void SetUpMap()
        {
            _map.ui = _ui;
            _map.OnUnitAdded += unit =>
            {
                unit.ui = _ui;
            };
            _map.OnUnitDead += () =>
            {
                int remainingBossCount = 0;
                var enemies = _map.enemyUnits;
                foreach (var enemy in enemies)
                    if (enemy.isBoss)
                        remainingBossCount++;

                if (remainingBossCount == 0)
                    if (OnBossWipedOut != null)
                        OnBossWipedOut();
            };

            _map.SetUp(_level);
        }

        private void SetUpPlayer()
        {
            _player = new Player();
            _player.OnUnitPlaced += unit =>
            {
                _map.AddUnit(unit);
            };
            _player.OnUnitDead += unit => 
            {
                if (unit.isPlayerUnit)
                    if (OnPlayerDead != null)
                        OnPlayerDead();
            };
            _player.SetUp(_level);

            _ui.mode.SetPlayer(_player);
        }
    }
}