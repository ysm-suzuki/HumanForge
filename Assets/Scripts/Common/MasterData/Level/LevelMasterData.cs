using System.Collections.Generic;

using UnityMVC;
using GameMain;

public class LevelMasterData
{
    public static LevelMasterDataLoader loader
    {
        get { return LevelMasterDataLoader.Instance; }
    }


    public int id;

    public int mapId;
    public int unitSetId;


    public Unit playerUnit
    {
        get
        {
            var initialUnits = UnitSetMasterData.loader.GetSet(unitSetId);
            foreach (var initialUnit in initialUnits)
            {
                var unit = initialUnit.ToUnit();

                if (!unit.isPlayerUnit)
                    continue;

                return unit;
            }

            return null;
        }
    }

    public List<Unit> ownedUnits
    {
        get
        {
            var units = new List<Unit>();

            var initialUnits = UnitSetMasterData.loader.GetSet(unitSetId);
            foreach (var initialUnit in initialUnits)
            {
                var unit = initialUnit.ToUnit();

                if (unit.isPlayerUnit)
                    continue;
                if (!unit.isOwnedUnit)
                    continue;

                units.Add(unit); ;
            }

            return units;
        }
    }

    public List<Unit> enemyUnits
    {
        get
        {
            var units = new List<Unit>();

            var initialUnits = UnitSetMasterData.loader.GetSet(unitSetId);
            foreach (var initialUnit in initialUnits)
            {
                var unit = initialUnit.ToUnit();

                if (unit.isPlayerUnit)
                    continue;
                if (unit.isOwnedUnit)
                    continue;

                units.Add(unit); ;
            }

            return units;
        }
    }
}