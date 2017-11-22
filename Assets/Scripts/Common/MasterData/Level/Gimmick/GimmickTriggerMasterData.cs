using System.Collections.Generic;

using UnityMVC;
using GameMain;

public class GimmickTriggerMasterData
{
    public static GimmickTriggerMasterDataLoader loader
    {
        get { return GimmickTriggerMasterDataLoader.Instance; }
    }


    public int id;

    public Gimmick.Trigger.Type type;
    public float value;

    public Gimmick.Trigger ToTrigger()
    {
        return new Gimmick.Trigger
        {
            type = type,
            value = value
        };
    }
}