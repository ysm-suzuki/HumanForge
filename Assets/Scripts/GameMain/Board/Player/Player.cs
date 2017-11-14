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
            var initialUnits = UnitSetMasterData.loader.GetSet(1);
            foreach(var initialUnit in initialUnits)
            {
                var unit = initialUnit.ToUnit();
                if (!unit.isPlayerUnit)
                    continue;

                PlaceUnit(unit);
                break;
            }
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