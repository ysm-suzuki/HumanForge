using System.Collections.Generic;

using SQLite.Attribute;

using UnityMVC;

public class GimmickTriggerSetMasterDataLoader : MasterDataLoader<GimmickTriggerSetMasterData>
{
    private static GimmickTriggerSetMasterDataLoader _instance = null;
    public static GimmickTriggerSetMasterDataLoader Instance
    {
        get { return _instance ?? new GimmickTriggerSetMasterDataLoader(); }
    }
    private GimmickTriggerSetMasterDataLoader() { }

    override public Dictionary<int, GimmickTriggerSetMasterData> GetAllData()
    {
        if (_data != null)
            return _data;
        
        _data = new Dictionary<int, GimmickTriggerSetMasterData>();

        var sqLite = GameMain.SQLite.Instance.connection;
        var list = sqLite.Table<gimmick_triger_set_data>();
        foreach (var item in list)
            _data[item.id] = Convert(item);

        return _data;
    }

    override public IEnumerable<GimmickTriggerSetMasterData> GetAll()
    {
        return GetAllData().Values;
    }

    override public GimmickTriggerSetMasterData Get(int id)
    {
        return base.Get(id);
    }

    public List<GimmickTriggerSetMasterData> GetSet(int setId)
    {
        // TODO : cache?

        var sqLite = GameMain.SQLite.Instance.connection;
        var list = sqLite
                    .Table<gimmick_triger_set_data>()
                    .Where(set => set.setId == setId);

        var setData = new List<GimmickTriggerSetMasterData>();
        foreach (var item in list)
            setData.Add(Convert(item));

        return setData;
    }

    private GimmickTriggerSetMasterData Convert(gimmick_triger_set_data data)
    {
        var convertedData = new GimmickTriggerSetMasterData();

        convertedData.id = data.id;

        convertedData.setId = data.setId;
        convertedData.triggerId = data.triggerId;

        return convertedData;
    }

    public class gimmick_triger_set_data
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        
        public int setId { get; set; }
        public int triggerId { get; set; }
    }
}
