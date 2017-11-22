using System.Collections.Generic;

using SQLite.Attribute;

using UnityMVC;

public class VisualNotificationSetMasterDataLoader : MasterDataLoader<VisualNotificationSetMasterData>
{
    private static VisualNotificationSetMasterDataLoader _instance = null;
    public static VisualNotificationSetMasterDataLoader Instance
    {
        get { return _instance ?? new VisualNotificationSetMasterDataLoader(); }
    }
    private VisualNotificationSetMasterDataLoader() { }

    override public Dictionary<int, VisualNotificationSetMasterData> GetAllData()
    {
        if (_data != null)
            return _data;
        
        _data = new Dictionary<int, VisualNotificationSetMasterData>();

        var sqLite = GameMain.SQLite.Instance.connection;
        var list = sqLite.Table<visual_notification_set_data>();
        foreach (var item in list)
            _data[item.id] = Convert(item);

        return _data;
    }

    override public IEnumerable<VisualNotificationSetMasterData> GetAll()
    {
        return GetAllData().Values;
    }

    override public VisualNotificationSetMasterData Get(int id)
    {
        return base.Get(id);
    }

    public List<VisualNotificationSetMasterData> GetSet(int setId)
    {
        // TODO : cache?

        var sqLite = GameMain.SQLite.Instance.connection;
        var list = sqLite
                    .Table<visual_notification_set_data>()
                    .Where(set => set.setId == setId);

        var setData = new List<VisualNotificationSetMasterData>();
        foreach (var item in list)
            setData.Add(Convert(item));

        return setData;
    }

    private VisualNotificationSetMasterData Convert(visual_notification_set_data data)
    {
        var convertedData = new VisualNotificationSetMasterData();

        convertedData.id = data.id;

        convertedData.setId = data.setId;
        convertedData.visualNotificationId = data.visualNotificationId;

        return convertedData;
    }

    public class visual_notification_set_data
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        
        public int setId { get; set; }
        public int visualNotificationId { get; set; }
    }
}
