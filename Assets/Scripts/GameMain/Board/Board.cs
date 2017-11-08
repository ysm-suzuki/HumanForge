using System.Collections.Generic;

using UnityMVC;

namespace GameMain
{
    public class Board
    {
        private Map _map = new Map();
        private BoardUI _ui = new BoardUI();

        private Player _player = null;

        public Board()
        {
            SetUpUI();
            SetUpMap();
            SetUpPlayer();


            // kari
            var units = new Dictionary<Unit, Position>
            {
                {
                    new Unit(new UnitData
                    {
                        life = 10,
                        attack = 1,
                        attackSpeed = 1.0f,
                        defence = 5,
                        moveSpeed = 10.0f * 10,
                        sizeRadius = 10.0f
                    }),
                    Position.Create(-200, -200)
                },
                {
                    new Unit(new UnitData
                    {
                        life = 10,
                        attack = 1,
                        attackSpeed = 1.0f,
                        defence = 5,
                        moveSpeed = 10.0f * 10,
                        sizeRadius = 10.0f
                    }),
                    Position.Create(-200, 100)
                },
                {
                    new Unit(new UnitData
                    {
                        life = 10,
                        attack = 1,
                        attackSpeed = 1.0f,
                        defence = 5,
                        moveSpeed = 10.0f * 10,
                        sizeRadius = 10.0f
                    }),
                    Position.Create(-200, 400)
                }
            };
            foreach(var unit in units)
            {
                unit.Key.position = unit.Value;
                _map.AddUnit(unit.Key);
            }
        }

        public void Tick(float delta)
        {
            _map.Tick(delta);
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
            };
        }

        private void SetUpMap()
        {
            _map.ui = _ui;
            _map.OnUnitAdded += unit =>
            {
                unit.ui = _ui;
            };

            _map.SetUp();
        }

        private void SetUpPlayer()
        {
            _player = new Player();
            _player.OnUnitPlaced += unit =>
            {
                _map.AddUnit(unit);
            };
            _player.SetUp();
        }
    }
}