using System.Collections.Generic;

using UnityMVC;
using GameMain;

public class VisualNotificationSetMasterData
{
    public static VisualNotificationSetMasterDataLoader loader
    {
        get { return VisualNotificationSetMasterDataLoader.Instance; }
    }


    public int id;

    public int setId;
    public int visualNotificationId;


    public VisualNotification ToVisualNotification()
    {
        return VisualNotificationMasterData.loader
                    .Get(visualNotificationId)
                    .ToVisualNotification();
    }
}