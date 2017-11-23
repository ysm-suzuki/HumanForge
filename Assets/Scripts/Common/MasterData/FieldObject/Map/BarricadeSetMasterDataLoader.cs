using System.Collections.Generic;

using SQLite.Attribute;

public class BarricadeSetMasterDataLoader : MasterDataLoader<BarricadeSetMasterData>
{
    private static BarricadeSetMasterDataLoader _instance = null;
    public static BarricadeSetMasterDataLoader Instance
    {
        get { return _instance ?? new BarricadeSetMasterDataLoader(); }
    }
    private BarricadeSetMasterDataLoader() { }

    override public Dictionary<int, BarricadeSetMasterData> GetAllData()
    {
        if (_data != null)
            return _data;
        
        _data = new Dictionary<int, BarricadeSetMasterData>();

        var sqLite = GameMain.SQLite.Instance.connection;
        var list = sqLite.Table<barricade_set_data>();
        foreach (var item in list)
            _data[item.id] = Convert(item);

        return _data;
    }

    override public IEnumerable<BarricadeSetMasterData> GetAll()
    {
        return GetAllData().Values;
    }

    override public BarricadeSetMasterData Get(int id)
    {
        return base.Get(id);
    }
    public List<BarricadeSetMasterData> GetList(int setId)
    {
        // TODO : cache?

        var sqLite = GameMain.SQLite.Instance.connection;
        var list = sqLite
                    .Table<barricade_set_data>()
                    .Where(set => set.setId == setId);

        var setData = new List<BarricadeSetMasterData>();
        foreach (var item in list)
            setData.Add(Convert(item));

        return setData;
    }

    private BarricadeSetMasterData Convert(barricade_set_data data)
    {
        var convertedData = new BarricadeSetMasterData();

        convertedData.id = data.id;
        convertedData.setId = data.setId;

        convertedData.barricadeId = data.barricadeId;
        convertedData.x = data.x;
        convertedData.y = data.y;

        return convertedData;
    }

    public class barricade_set_data
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public int setId { get; set; }

        public int barricadeId { get; set; }
        public float x { get; set; }
        public float y { get; set; }
    }
}
