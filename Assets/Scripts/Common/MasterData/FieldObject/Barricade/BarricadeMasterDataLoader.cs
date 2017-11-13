using System.Collections.Generic;

using SQLite.Attribute;

public class BarricadeMasterDataLoader : MasterDataLoader<BarricadeMasterData>
{
    private static BarricadeMasterDataLoader _instance = null;
    public static BarricadeMasterDataLoader Instance
    {
        get { return _instance ?? new BarricadeMasterDataLoader(); }
    }
    private BarricadeMasterDataLoader() { }

    override public Dictionary<int, BarricadeMasterData> GetAllData()
    {
        if (_data != null)
            return _data;
        
        _data = new Dictionary<int, BarricadeMasterData>();

        var sqLite = GameMain.SQLite.Instance.connection;
        var list = sqLite.Table<barricade_data>();
        foreach (var item in list)
        {
            var data = new BarricadeMasterData();

            data.id = item.id;
            data.life = item.life;
            data.defence = item.defence;
            data.sizeRadius = item.sizeRadius;
            data.shapePointsId = item.shapePointsId;

            _data[data.id] = data;
        }

        return _data;
    }

    override public IEnumerable<BarricadeMasterData> GetAll()
    {
        return GetAllData().Values;
    }

    override public BarricadeMasterData Get(int id)
    {
        return base.Get(id);
    }

    public class barricade_data
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public float life { get; set; }
        public float defence { get; set; }
        public float sizeRadius { get; set; }
        public int shapePointsId { get; set; }

    }
}
