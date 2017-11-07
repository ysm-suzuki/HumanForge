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
            // ui
            SetUpUI();

            // map
            _map.SetUp();

            // player
            _player = new Player();
            _player.OnUnitPlaced += unit =>
            {
                _map.AddUnit(unit);
            };
            _player.SetUp();
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
            _ui.Initialize(_map);

            _map.ui = _ui;

            _map.OnUnitAdded += unit =>
            {
                unit.ui = _ui;
            };
        }
    }
}