using System.Collections.Generic;

using UnityMVC;

namespace GameMain
{
    public class Recognition
    {
        private List<Unit> _units = new List<Unit>();
        private List<Wall> _walls = new List<Wall>();
        private List<Barricade> _barricades = new List<Barricade>();

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
    }
}