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
        {
            var data = new BarricadeSetMasterData();

            data.id = item.id;

            data.barricadeId = item.barricadeId;
            data.x = item.x;
            data.y = item.y;
            data.isTerminal = item.isTerminal > 0;

            _data[data.id] = data;
        }

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

    public class barricade_set_data
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        public int barricadeId { get; set; }
        public float x { get; set; }
        public float y { get; set; }
        public int isTerminal { get; set; }
    }
}
