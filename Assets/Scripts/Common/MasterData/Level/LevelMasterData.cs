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
    public int gimmickSetId;


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


                units.Add(unit);
            }

            return units;
        }
    }

    public List<Gimmick> gimmicks
    {
        get
        {
            var targetGimmicks = new List<Gimmick>();
            var gimmickSet = GimmickSetMasterData.loader.GetSet(gimmickSetId);
            foreach (var gimmickData in gimmickSet)
                targetGimmicks.Add(gimmickData.ToGimmick());

            return targetGimmicks;
        }
    }
}