using System.Collections.Generic;

using SQLite.Attribute;

using UnityMVC;

public class VisualNotificationTriggerMasterDataLoader : MasterDataLoader<VisualNotificationTriggerMasterData>
{
    private static VisualNotificationTriggerMasterDataLoader _instance = null;
    public static VisualNotificationTriggerMasterDataLoader Instance
    {
        get { return _instance ?? new VisualNotificationTriggerMasterDataLoader(); }
    }
    private VisualNotificationTriggerMasterDataLoader() { }

    override public Dictionary<int, VisualNotificationTriggerMasterData> GetAllData()
    {
        if (_data != null)
            return _data;
        
        _data = new Dictionary<int, VisualNotificationTriggerMasterData>();

        var sqLite = GameMain.SQLite.Instance.connection;
        var list = sqLite.Table<visual_notification_trigger_data>();
        foreach (var item in list)
        {
            var data = new VisualNotificationTriggerMasterData();

            data.id = item.id;

            data.type = (GameMain.VisualNotification.Trigger.Type)item.type;
            data.value = item.value;

            _data[data.id] = data;
        }

        return _data;
    }

    override public IEnumerable<VisualNotificationTriggerMasterData> GetAll()
    {
        return GetAllData().Values;
    }

    override public VisualNotificationTriggerMasterData Get(int id)
    {
        return base.Get(id);
    }

    public class visual_notification_trigger_data
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        public int type { get; set; }
        public float value { get; set; }
    }
}
