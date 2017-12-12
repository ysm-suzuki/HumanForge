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
    public int visualNotificationSetId;


    public Unit playerUnit
    {
        get
        {
            var initialUnits = UnitSetMasterData.loader.GetList(unitSetId);
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

            var initialUnits = UnitSetMasterData.loader.GetList(unitSetId);
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

            var initialUnits = UnitSetMasterData.loader.GetList(unitSetId);
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
            var gimmickSet = GimmickSetMasterData.loader.GetList(gimmickSetId);
            foreach (var gimmickData in gimmickSet)
                targetGimmicks.Add(gimmickData.ToGimmick());

            return targetGimmicks;
        }
    }

    public List<VisualNotification> visualNotifications
    {
        get
        {
            var targetNotifications = new List<VisualNotification>();
            var visualNotificationSet = VisualNotificationSetMasterData.loader.GetList(visualNotificationSetId);
            foreach (var visualNotificationData in visualNotificationSet)
                targetNotifications.Add(visualNotificationData.ToVisualNotification());

            return targetNotifications;
        }
    }
}