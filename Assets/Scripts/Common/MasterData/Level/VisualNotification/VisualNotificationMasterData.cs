using System.Collections.Generic;

using UnityMVC;
using GameMain;

public class VisualNotificationMasterData
{
    public static VisualNotificationMasterDataLoader loader
    {
        get { return VisualNotificationMasterDataLoader.Instance; }
    }


    public int id;

    public int triggerSetId;
    public VisualNotification.Product.Type productType;
    public int number;
    public string text;

    public VisualNotification ToVisualNotification()
    {
        var triggers = new List<VisualNotification.Trigger>();
        var triggerSet = VisualNotificationTriggerSetMasterData.loader.GetSet(triggerSetId);
        foreach (var triggerData in triggerSet)
            triggers.Add(triggerData.ToTrigger());

        return new VisualNotification(
                triggers,
                new VisualNotification.Product
                {
                    type = productType,
                    number = number,
                    text = text
                });
    }
}