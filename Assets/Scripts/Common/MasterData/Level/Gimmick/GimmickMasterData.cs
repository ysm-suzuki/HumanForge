using System.Collections.Generic;

using UnityMVC;
using GameMain;

public class GimmickMasterData
{
    public static GimmickMasterDataLoader loader
    {
        get { return GimmickMasterDataLoader.Instance; }
    }


    public int id;

    public int triggerSetId;
    public Gimmick.Product.Type productType;

    public Gimmick ToGimmick()
    {
        var triggers = new List<Gimmick.Trigger>();
        var triggerSet = GimmickTriggerSetMasterData.loader.GetList(triggerSetId);
        foreach (var triggerData in triggerSet)
            triggers.Add(triggerData.ToTrigger());

        return new Gimmick(
                triggers,
                new Gimmick.Product
                {
                    type = productType
                });
    }
}