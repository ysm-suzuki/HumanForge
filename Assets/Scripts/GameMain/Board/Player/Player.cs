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
            const float PI = UnityEngine.Mathf.PI;
            var unit = new Unit(UnitMasterData.loader.Get(1).ToUnitData());
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