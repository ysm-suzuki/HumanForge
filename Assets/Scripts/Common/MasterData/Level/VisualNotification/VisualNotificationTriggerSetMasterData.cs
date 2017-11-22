using System.Collections.Generic;

using UnityMVC;
using GameMain;

public class VisualNotificationTriggerSetMasterData
{
    public static VisualNotificationTriggerSetMasterDataLoader loader
    {
        get { return VisualNotificationTriggerSetMasterDataLoader.Instance; }
    }


    public int id;

    public int setId;
    public int triggerId;


    public VisualNotification.Trigger ToTrigger()
    {
        return VisualNotificationTriggerMasterData
                    .loader
                    .Get(triggerId)
                    .ToTrigger();
    }
}