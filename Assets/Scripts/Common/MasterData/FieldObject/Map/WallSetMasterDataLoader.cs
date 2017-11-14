using System.Collections.Generic;

using SQLite.Attribute;

public class WallSetMasterDataLoader : MasterDataLoader<WallSetMasterData>
{
    private static WallSetMasterDataLoader _instance = null;
    public static WallSetMasterDataLoader Instance
    {
        get { return _instance ?? new WallSetMasterDataLoader(); }
    }
    private WallSetMasterDataLoader() { }

    override public Dictionary<int, WallSetMasterData> GetAllData()
    {
        if (_data != null)
            return _data;
        
        _data = new Dictionary<int, WallSetMasterData>();

        var sqLite = GameMain.SQLite.Instance.connection;
        var list = sqLite.Table<wall_set_data>();
        foreach (var item in list)
        {
            var data = new WallSetMasterData();

            data.id = item.id;

            data.shapeId = item.shapeId;
            data.x = item.x;
            data.y = item.y;
            data.rotationTheta = item.rotationTheta;
            data.isTerminal = item.isTerminal > 0;

            _data[data.id] = data;
        }

        return _data;
    }

    override public IEnumerable<WallSetMasterData> GetAll()
    {
        return GetAllData().Values;
    }

    override public WallSetMasterData Get(int id)
    {
        return base.Get(id);
    }

    public class wall_set_data
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        public int shapeId { get; set; }
        public float x { get; set; }
        public float y { get; set; }
        public float rotationTheta { get; set; }
        public int isTerminal { get; set; }
    }
}
