using System.Collections.Generic;

using UnityMVC;

namespace GameMain
{
    public class Board
    {
        private Map _map = new Map();


        public Board()
        {
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

            
            _map.AddUnit(unit);
            _map.AddWall(wall);
        }

        public void Tick(float delta)
        {
            _map.Tick(delta);
        }
        

        public Map map
        {
            get { return _map; }
        }
    }
}