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
    public int groupeId;

    public int unitId;
    public float x;
    public float y;
    public int individualAttributeFlags;

    public Unit ToUnit()
    {
        var data = UnitMasterData.loader.Get(unitId).ToUnitData();
        data.groupeId = groupeId;
        var unit = new Unit(data);
        unit.position = Position.Create(x, y);
        unit.SetIndividualAttribute(individualAttributeFlags);

        return unit;
    }
}