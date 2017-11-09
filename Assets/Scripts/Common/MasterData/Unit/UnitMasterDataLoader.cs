using System.Collections.Generic;

using SQLite.Attribute;

public class UnitMasterDataLoader : MasterDataLoader<UnitMasterData>
{
    private static UnitMasterDataLoader _instance = null;
    public static UnitMasterDataLoader Instance
    {
        get { return _instance ?? new UnitMasterDataLoader(); }
    }
    private UnitMasterDataLoader() { }

    override public Dictionary<int, UnitMasterData> GetAllData()
    {
        if (_data != null)
            return _data;


        _data = new Dictionary<int, UnitMasterData>();

        var sqLite = GameMain.SQLite.Instance.connection;
        var list = sqLite.Table<unit_data>();
        foreach (var item in list)
        {
            var data = new UnitMasterData();

            _data[data.id] = data;
        }

        return _data;
    }

    override public IEnumerable<UnitMasterData> GetAll()
    {
        return GetAllData().Values;
    }

    override public UnitMasterData Get(int id)
    {
        return base.Get(id);
    }

    public class unit_data
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
    }
}
