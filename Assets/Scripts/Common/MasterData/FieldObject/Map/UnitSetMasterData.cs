using System.Collections.Generic;

using UnityMVC;
using GameMain;

public class UnitSetMasterData
{
    public static UnitSetMasterDataLoader loader
    {
        get { return UnitSetMasterDataLoader.Instance; }
    }

    public int id;
    public int setId;

    public int unitId;
    public float x;
    public float y;
    public int individualAttributeFlags;

    public Unit ToUnit()
    {
        var unit = new Unit(UnitMasterData.loader.Get(unitId).ToUnitData());
        unit.position = Position.Create(x, y);

        var individualAttribute = new IndividualAttribute().Set(individualAttributeFlags);

        unit.isPlayerUnit = individualAttribute.isPlayerUnit;
        unit.isOwnedUnit = individualAttribute.isOwned;

        return unit;
    }
}