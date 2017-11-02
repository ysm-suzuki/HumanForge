using System.Collections.Generic;

using UnityMVC;

namespace GameMain
{
    public class MainPhase : Phase
    {
        public static string Tag = "MainPhase";

        private Board _board = new Board();

        override public void Initialize()
        {
            // kari
            var unit = new Unit(new UnitData
            {
                life = 10,
                attack = 5,
                attackSpeed = 1.0f,
                defence = 5,
                moveSpeed = 10.0f * 20
            });

            UnitView
                .Attach(ViewManager.Instance.GetRoot(GameMainKicker.BoardRootTag))
                .SetModel(unit);

            _board.AddUnit(unit);
            unit.position = Position.Create(-150, 5);
            unit.MoveTo(Position.Create(150, 0));

            var wall = new Wall();
            wall.shapePoints = new List<Position>
            {
                Position.Create(-40, 50),
                Position.Create(0, 50),
                Position.Create(40, -50),
                Position.Create(0, -50),
            };
            wall.position = Position.Create(60, 0);
            _board.AddWall(wall);

            DebugShapeView
                .Attach(ViewManager.Instance.GetRoot(GameMainKicker.BoardRootTag))
                .SetModel(unit);
            DebugShapeView
                .Attach(ViewManager.Instance.GetRoot(GameMainKicker.BoardRootTag))
                .SetModel(wall);
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
