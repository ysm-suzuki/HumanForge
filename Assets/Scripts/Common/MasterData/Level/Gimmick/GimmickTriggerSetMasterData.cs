using System.Collections.Generic;

using UnityMVC;
using GameMain;

public class GimmickTriggerSetMasterData
{
    public static GimmickTriggerSetMasterDataLoader loader
    {
        get { return GimmickTriggerSetMasterDataLoader.Instance; }
    }


    public int id;

    public int setId;
    public int triggerId;


    public Gimmick.Trigger ToTrigger()
    {
        return GimmickTriggerMasterData
                    .loader
                    .Get(triggerId)
                    .ToTrigger();
    }
}