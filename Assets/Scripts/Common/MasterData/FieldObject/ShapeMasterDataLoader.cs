using System.Collections.Generic;

using SQLite.Attribute;

using UnityMVC;

public class ShapeMasterDataLoader : MasterDataLoader<ShapeMasterData>
{
    private static ShapeMasterDataLoader _instance = null;
    public static ShapeMasterDataLoader Instance
    {
        get { return _instance ?? new ShapeMasterDataLoader(); }
    }
    private ShapeMasterDataLoader() { }

    override public Dictionary<int, ShapeMasterData> GetAllData()
    {
        if (_data != null)
            return _data;
        
        _data = new Dictionary<int, ShapeMasterData>();

        var sqLite = GameMain.SQLite.Instance.connection;
        var list = sqLite.Table<shape_data>();
        foreach (var item in list)
        {
            var data = new ShapeMasterData();

            data.id = item.id;

            var positions = new List<Position>();
            var positionSet = VertexSetMasterData.loader.GetSet(item.positionSetId);
            foreach (var vertex in positionSet)
                positions.Add(vertex.ToPosition());

            data.positions = positions;

            _data[data.id] = data;
        }

        return _data;
    }

    override public IEnumerable<ShapeMasterData> GetAll()
    {
        return GetAllData().Values;
    }

    override public ShapeMasterData Get(int id)
    {
        return base.Get(id);
    }

    public class shape_data
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public int positionSetId { get; set; }
    }
}
