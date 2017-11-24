using System.Collections.Generic;

using SQLite.Attribute;

public class UnitSetMasterDataLoader : MasterDataLoader<UnitSetMasterData>
{
    private static UnitSetMasterDataLoader _instance = null;
    public static UnitSetMasterDataLoader Instance
    {
        get { return _instance ?? new UnitSetMasterDataLoader(); }
    }
    private UnitSetMasterDataLoader() { }

    override public Dictionary<int, UnitSetMasterData> GetAllData()
    {
        if (_data != null)
            return _data;
        
        _data = new Dictionary<int, UnitSetMasterData>();

        var sqLite = GameMain.SQLite.Instance.connection;
        var list = sqLite.Table<unit_set_data>();
        foreach (var item in list)
            _data[item.id] = Convert(item);

        return _data;
    }

    override public IEnumerable<UnitSetMasterData> GetAll()
    {
        return GetAllData().Values;
    }

    override public UnitSetMasterData Get(int id)
    {
        return base.Get(id);
    }
    public List<UnitSetMasterData> GetList(int setId)
    {
        // TODO : cache?

        var sqLite = GameMain.SQLite.Instance.connection;
        var list = sqLite
                    .Table<unit_set_data>()
                    .Where(set => set.setId == setId);

        var setData = new List<UnitSetMasterData>();
        foreach (var item in list)
            setData.Add(Convert(item));

        return setData;
    }

    private UnitSetMasterData Convert(unit_set_data data)
    {
        var convertedData = new UnitSetMasterData();

        convertedData.id = data.id;
        convertedData.setId = data.setId;

        convertedData.unitId = data.unitId;
        convertedData.x = data.x;
        convertedData.y = data.y;
        convertedData.individualAttributeFlags = data.individualAttributeFlags;

        return convertedData;
    }

    public class unit_set_data
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public int setId { get; set; }

        public int unitId { get; set; }
        public float x { get; set; }
        public float y { get; set; }
        public int individualAttributeFlags { get; set; }
    }
}
