using System.Collections.Generic;

using SQLite.Attribute;

using UnityMVC;

public class VertexSetMasterDataLoader : MasterDataLoader<VertexSetMasterData>
{
    private static VertexSetMasterDataLoader _instance = null;
    public static VertexSetMasterDataLoader Instance
    {
        get { return _instance ?? new VertexSetMasterDataLoader(); }
    }
    private VertexSetMasterDataLoader() { }

    override public Dictionary<int, VertexSetMasterData> GetAllData()
    {
        if (_data != null)
            return _data;
        
        _data = new Dictionary<int, VertexSetMasterData>();

        var sqLite = GameMain.SQLite.Instance.connection;
        var list = sqLite.Table<vertex_set_data>();
        foreach (var item in list)
            _data[item.id] = Convert(item);

        return _data;
    }

    override public IEnumerable<VertexSetMasterData> GetAll()
    {
        return GetAllData().Values;
    }

    override public VertexSetMasterData Get(int id)
    {
        return base.Get(id);
    }
    public List<VertexSetMasterData> GetSet(int setId)
    {
        var sqLite = GameMain.SQLite.Instance.connection;
        var list = sqLite
                    .Table<vertex_set_data>()
                    .Where(set => set.setId == setId);

        var dataSet = new List<VertexSetMasterData>();
        foreach (var item in list)
            dataSet.Add(Convert(item));

        return dataSet;
    }

    private VertexSetMasterData Convert(vertex_set_data raw)
    {
        var data = new VertexSetMasterData();

        data.id = raw.id;
        data.x = raw.x;
        data.y = raw.y;

        return data;
    }

    public class vertex_set_data
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public int setId { get; set; }

        public float x { get; set; }
        public float y { get; set; }
    }
}
