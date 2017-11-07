using System.Collections.Generic;

using UnityMVC;

namespace GameMain
{
    public class MainPhase : Phase
    {
        public static string Tag = "MainPhase";

        private Board _board = new Board();

        private MapView _mapView = null;

        override public void Initialize()
        {
            if (_mapView == null)
                _mapView = MapView
                    .Attach(ViewManager.Instance.GetRoot(GameMainKicker.BoardRootTag))
                    .SetModel(_board.map);

            // kari
            var unit = new Unit(new UnitData
            {
                life = 10,
                attack = 5,
                attackSpeed = 1.0f,
                defence = 5,
                moveSpeed = 10.0f * 10,
                sizeRadius = 10.0f
            });
            unit.position = Position.Create(150, 0);
            unit.MoveTo(Position.Create(-150, 0));


            var wall = new Wall();
            wall.shapePoints = new List<Position>
            {
                Position.Create(-40, 50),
                Position.Create(0, 50),
                Position.Create(40, -50),
                Position.Create(0, -50),
            };
            wall.position = Position.Create(60, 0);



            _board.map.AddUnit(unit);
            _board.map.AddWall(wall);
        }

        override public void Tick(float delta)
        {
            _board.Tick(delta);
        }

        override protected void End()
        {
            base.End(ReadyPhase.Tag);
        }
    }
}
