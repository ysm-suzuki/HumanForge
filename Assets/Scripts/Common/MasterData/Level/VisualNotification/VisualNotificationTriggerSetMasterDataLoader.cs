using System.Collections.Generic;

using SQLite.Attribute;

using UnityMVC;

public class VisualNotificationTriggerSetMasterDataLoader : MasterDataLoader<VisualNotificationTriggerSetMasterData>
{
    private static VisualNotificationTriggerSetMasterDataLoader _instance = null;
    public static VisualNotificationTriggerSetMasterDataLoader Instance
    {
        get { return _instance ?? new VisualNotificationTriggerSetMasterDataLoader(); }
    }
    private VisualNotificationTriggerSetMasterDataLoader() { }

    override public Dictionary<int, VisualNotificationTriggerSetMasterData> GetAllData()
    {
        if (_data != null)
            return _data;
        
        _data = new Dictionary<int, VisualNotificationTriggerSetMasterData>();

        var sqLite = GameMain.SQLite.Instance.connection;
        var list = sqLite.Table<visual_notification_trigger_set_data>();
        foreach (var item in list)
            _data[item.id] = Convert(item);

        return _data;
    }

    override public IEnumerable<VisualNotificationTriggerSetMasterData> GetAll()
    {
        return GetAllData().Values;
    }

    override public VisualNotificationTriggerSetMasterData Get(int id)
    {
        return base.Get(id);
    }

    public List<VisualNotificationTriggerSetMasterData> GetList(int setId)
    {
        // TODO : cache?

        var sqLite = GameMain.SQLite.Instance.connection;
        var list = sqLite
                    .Table<visual_notification_trigger_set_data>()
                    .Where(set => set.setId == setId);

        var setData = new List<VisualNotificationTriggerSetMasterData>();
        foreach (var item in list)
            setData.Add(Convert(item));

        return setData;
    }

    private VisualNotificationTriggerSetMasterData Convert(visual_notification_trigger_set_data data)
    {
        var convertedData = new VisualNotificationTriggerSetMasterData();

        convertedData.id = data.id;

        convertedData.setId = data.setId;
        convertedData.triggerId = data.triggerId;

        return convertedData;
    }

    public class visual_notification_trigger_set_data
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        
        public int setId { get; set; }
        public int triggerId { get; set; }
    }
}
