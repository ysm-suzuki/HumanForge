using System.Collections.Generic;

using SQLite.Attribute;

using UnityMVC;

public class VisualNotificationMasterDataLoader : MasterDataLoader<VisualNotificationMasterData>
{
    private static VisualNotificationMasterDataLoader _instance = null;
    public static VisualNotificationMasterDataLoader Instance
    {
        get { return _instance ?? new VisualNotificationMasterDataLoader(); }
    }
    private VisualNotificationMasterDataLoader() { }

    override public Dictionary<int, VisualNotificationMasterData> GetAllData()
    {
        if (_data != null)
            return _data;
        
        _data = new Dictionary<int, VisualNotificationMasterData>();

        var sqLite = GameMain.SQLite.Instance.connection;
        var list = sqLite.Table<visual_notification_data>();
        foreach (var item in list)
        {
            var data = new VisualNotificationMasterData();

            data.id = item.id;

            data.triggerSetId = item.triggerSetId;
            data.productType = (GameMain.VisualNotification.Product.Type)item.productType;
            data.number = item.number;
            data.text = item.text;

            _data[data.id] = data;
        }

        return _data;
    }

    override public IEnumerable<VisualNotificationMasterData> GetAll()
    {
        return GetAllData().Values;
    }

    override public VisualNotificationMasterData Get(int id)
    {
        return base.Get(id);
    }

    public class visual_notification_data
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        public int triggerSetId { get; set; }
        public int productType { get; set; }
        public int number { get; set; }
        public string text { get; set; }
    }
}
