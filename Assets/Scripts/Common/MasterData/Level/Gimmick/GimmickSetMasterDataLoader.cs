using System.Collections.Generic;

using SQLite.Attribute;

using UnityMVC;

public class GimmickSetMasterDataLoader : MasterDataLoader<GimmickSetMasterData>
{
    private static GimmickSetMasterDataLoader _instance = null;
    public static GimmickSetMasterDataLoader Instance
    {
        get { return _instance ?? new GimmickSetMasterDataLoader(); }
    }
    private GimmickSetMasterDataLoader() { }

    override public Dictionary<int, GimmickSetMasterData> GetAllData()
    {
        if (_data != null)
            return _data;
        
        _data = new Dictionary<int, GimmickSetMasterData>();

        var sqLite = GameMain.SQLite.Instance.connection;
        var list = sqLite.Table<gimmick_set_data>();
        foreach (var item in list)
            _data[item.id] = Convert(item);

        return _data;
    }

    override public IEnumerable<GimmickSetMasterData> GetAll()
    {
        return GetAllData().Values;
    }

    override public GimmickSetMasterData Get(int id)
    {
        return base.Get(id);
    }

    public List<GimmickSetMasterData> GetList(int setId)
    {
        // TODO : cache?

        var sqLite = GameMain.SQLite.Instance.connection;
        var list = sqLite
                    .Table<gimmick_set_data>()
                    .Where(set => set.setId == setId);

        var setData = new List<GimmickSetMasterData>();
        foreach (var item in list)
            setData.Add(Convert(item));

        return setData;
    }

    private GimmickSetMasterData Convert(gimmick_set_data data)
    {
        var convertedData = new GimmickSetMasterData();

        convertedData.id = data.id;

        convertedData.setId = data.setId;
        convertedData.gimmickId = data.gimmickId;

        return convertedData;
    }

    public class gimmick_set_data
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        
        public int setId { get; set; }
        public int gimmickId { get; set; }
    }
}
