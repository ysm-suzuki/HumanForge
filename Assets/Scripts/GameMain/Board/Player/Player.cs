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
                attack = 2,
                defence = 5,
                moveSpeed = 10.0f * 10,
                sizeRadius = 10.0f,
                sightRange = 300,
                attackActions = new List<UnitAttackData>
                {
                    new UnitAttackData
                    {
                        power = 1,
                        range = 50,
                        warmUpSeconds = 0.1f,
                        coolDownSeconds = 0.5f
                    }
                }
            });
            unit.position = Position.Create(150, 0);
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