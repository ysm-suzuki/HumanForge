using System.Collections.Generic;

using UnityMVC;

namespace GameMain
{
    public class Recognition
    {
        private Unit _owner = null;

        private List<Unit> _units = new List<Unit>();
        private List<Wall> _walls = new List<Wall>();
        private List<Barricade> _barricades = new List<Barricade>();


        public Recognition(Unit owner)
        {
            _owner = owner;
        }


        public void UpdateUnits(List<Unit> units)
        {
            _units.Clear();
            _units.AddRange(units);
        }

        public void UpdateWalls(List<Wall> walls)
        {
            _walls.Clear();
            _walls.AddRange(walls);
        }

        public void UpdateBarricades(List<Barricade> barricades)
        {
            _barricades.Clear();
            _barricades.AddRange(barricades);
        }

        public List<Unit> GetEnemyUnits()
        {
            var enemies = new List<Unit>();
            foreach (var unit in _units)
                if (unit.isAlive
                    && IsOpposite(unit))
                    enemies.Add(unit);

            return enemies;
        }

        public FieldObject GetTopPriorityTarget()
        {
            float minDistance = float.MaxValue;
            FieldObject target = null;

            foreach (var unit in GetEnemyUnits())
            {
                float distance = (unit.position - _owner.position).ToVector().GetLength();

                if (distance >= minDistance)
                    continue;

                minDistance = distance;
                target = unit;
            }

            if (target != null)
                return target;


            return target;
        }


        private bool IsOpposite(FieldObject target)
        {
            return !_owner.IsFriendlyTeam(target);
        }
    }
}