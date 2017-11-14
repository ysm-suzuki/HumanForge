using System.Collections.Generic;

using UnityMVC;

namespace GameMain
{
    public class Player
    {
        public delegate void UnitEventHandler(Unit unit);
        public event UnitEventHandler OnUnitPlaced;
        public event UnitEventHandler OnUnitDead;

        public void SetUp(int level)
        {
            var levelData = LevelMasterData.loader.Get(level);

            var playerUnit = levelData.playerUnit;

            UnityEngine.Debug.Assert(playerUnit != null, "The player unit not set at level " + level);
            
            PlaceUnit(playerUnit);


            foreach (var unit in levelData.ownedUnits)
                PlaceUnit(unit);
        }

        public void PlaceUnit(Unit unit)
        {
            unit.isOwnedUnit = true;

            unit.OnDead += () => 
            {
                if (OnUnitDead != null)
                    OnUnitDead(unit);
            };

            if (OnUnitPlaced != null)
                OnUnitPlaced(unit);
        }
    }
}