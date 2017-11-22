using System.Collections.Generic;

using UnityMVC;
using GameMain;

public class VisualNotificationTriggerMasterData
{
    public static VisualNotificationTriggerMasterDataLoader loader
    {
        get { return VisualNotificationTriggerMasterDataLoader.Instance; }
    }


    public int id;

    public VisualNotification.Trigger.Type type;
    public float value;

    public VisualNotification.Trigger ToTrigger()
    {
        return new VisualNotification.Trigger
        {
            type = type,
            value = value
        };
    }
}