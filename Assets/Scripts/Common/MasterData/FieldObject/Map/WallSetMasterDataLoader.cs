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
            _data[item.id] = Convert(item);

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
    public List<WallSetMasterData> GetList(int setId)
    {
        // TODO : cache?

        var sqLite = GameMain.SQLite.Instance.connection;
        var list = sqLite
                    .Table<wall_set_data>()
                    .Where(set => set.setId == setId);

        var setData = new List<WallSetMasterData>();
        foreach (var item in list)
            setData.Add(Convert(item));

        return setData;
    }

    private WallSetMasterData Convert(wall_set_data data)
    {
        var convertedData = new WallSetMasterData();

        convertedData.id = data.id;
        convertedData.setId = data.setId;

        convertedData.shapeId = data.shapeId;
        convertedData.x = data.x;
        convertedData.y = data.y;
        convertedData.rotationTheta = data.rotationTheta;

        return convertedData;
    }

    public class wall_set_data
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public int setId { get; set; }

        public int shapeId { get; set; }
        public float x { get; set; }
        public float y { get; set; }
        public float rotationTheta { get; set; }
    }
}
