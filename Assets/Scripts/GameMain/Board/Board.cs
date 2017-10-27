using System.Collections.Generic;

using UnityMVC;

namespace GameMain
{
    public class Board
    {
        private List<Unit> _units = new List<Unit>();

        public void Tick(float delta)
        {
            foreach (var unit in _units)
                unit.Tick(delta);
        }
    }
}