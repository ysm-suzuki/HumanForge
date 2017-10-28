using System.Collections.Generic;

using UnityMVC;

namespace GameMain
{
    public class Board
    {
        private List<Wall> _walls = new List<Wall>();
        private List<Unit> _units = new List<Unit>();

        private List<Wall> _removingWalls = new List<Wall>();
        private List<Unit> _removingUnits = new List<Unit>();

        public void Tick(float delta)
        {
            foreach (var unit in _units)
                unit.Tick(delta);

            SolveMoving();

            foreach (var removnigUnit in _removingUnits)
                _units.Remove(removnigUnit);
            _removingUnits.Clear();
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

        public void RemoveUnit(Unit unit)
        {
            _removingUnits.Add(unit);
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