using System.Collections.Generic;

using UnityMVC;

namespace GameMain
{
    public class Board
    {
        private List<Wall> _walls = new List<Wall>();
        private List<Unit> _units = new List<Unit>();

        public void Tick(float delta)
        {
            foreach (var unit in _units)
                unit.Tick(delta);

            SolveMoving();
        }


        public void AddUnit(Unit unit)
        {
            if (_units.Contains(unit))
                return;
            foreach (var aUnit in _units)
                if (unit.IsSame(aUnit))
                    return;

            _units.Add(unit);
        }



        private void SolveMoving()
        {
            foreach (var unit in _units)
            {
                unit.position = GetNextPosition(unit.position, unit.velocity);
                unit.velocity = Position.Create(0, 0);
            }
        }


        private Position GetNextPosition(
            Position currentPosition,
            Position velocity)
        {
            // kari
            return currentPosition + velocity;
        }
    }
}