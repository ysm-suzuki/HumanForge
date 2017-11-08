using System.Collections.Generic;

using UnityMVC;

namespace GameMain
{
    public class Player
    {
        public delegate void UnitEventHandler(Unit unit);
        public event UnitEventHandler OnUnitPlaced;
        
        public void SetUp()
        {
            // kari
            var unit = new Unit(new UnitData
            {
                life = 20,
                attack = 3,
                attackSpeed = 1.0f,
                defence = 5,
                moveSpeed = 10.0f * 10,
                sizeRadius = 10.0f
            });
            unit.position = Position.Create(150, 0);
            unit.MoveTo(Position.Create(-150, 0));
            unit.isPlayerUnit = true;

            PlaceUnit(unit);
        }

        public void PlaceUnit(Unit unit)
        {
            unit.isOwnedUnit = true;
            unit.isPlayerUnit = true;

            if (OnUnitPlaced != null)
                OnUnitPlaced(unit);
        }
    }
}