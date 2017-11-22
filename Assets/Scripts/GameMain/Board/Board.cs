using System.Collections.Generic;

using UnityMVC;

namespace GameMain
{
    public class Board
    {
        public delegate void EventHandler();
        public event EventHandler OnMapStarted;
        public event EventHandler OnBossWipedOut;
        public event EventHandler OnPlayerDead;

        public delegate void TimeEventHandler(int seconds);
        public event TimeEventHandler OnTimeTicked;


        private Map _map = new Map();
        private BoardUI _ui = new BoardUI();

        private Player _player = null;

        private int _level = 1;


        // timekeeper
        private float _elaspedSeconds = 0;


        public Board()
        {
            _level = 1; // kari
            
            _elaspedSeconds = 0;
            
            SetUpUI();
            SetUpMap();
            SetUpPlayer();
        }

        public void Start()
        {
            if (OnMapStarted != null)
                OnMapStarted();
        }

        public void Tick(float delta)
        {
            // time keeper
            float newSeconds = _elaspedSeconds + delta;
            int elaspedSecondsI = UnityEngine.Mathf.FloorToInt(_elaspedSeconds);
            int newSecondsI = UnityEngine.Mathf.FloorToInt(newSeconds);
            if (elaspedSecondsI != newSecondsI)
                if (OnTimeTicked != null)
                    OnTimeTicked(newSecondsI);
            _elaspedSeconds = newSeconds;


            _map.Tick(delta);

            if (_player != null)
                _player.Tick(delta);
        }

        public void AddInitialEnemies()
        {
            var levelData = LevelMasterData.loader.Get(_level);
            const int EnemyTeamId = 2; // kari
            foreach (var unit in levelData.enemyUnits)
            {
                unit.teamId = EnemyTeamId;
                map.AddUnit(unit);
            }
        }
        

        public Map map
        {
            get { return _map; }
        }
        public int level
        {
            get { return _level; }
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