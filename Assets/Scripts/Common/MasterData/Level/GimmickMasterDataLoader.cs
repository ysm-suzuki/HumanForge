using System.Collections.Generic;

using SQLite.Attribute;

using UnityMVC;

public class GimmickMasterDataLoader : MasterDataLoader<GimmickMasterData>
{
    private static GimmickMasterDataLoader _instance = null;
    public static GimmickMasterDataLoader Instance
    {
        get { return _instance ?? new GimmickMasterDataLoader(); }
    }
    private GimmickMasterDataLoader() { }

    override public Dictionary<int, GimmickMasterData> GetAllData()
    {
        if (_data != null)
            return _data;
        
        _data = new Dictionary<int, GimmickMasterData>();

        var sqLite = GameMain.SQLite.Instance.connection;
        var list = sqLite.Table<gimmick_data>();
        foreach (var item in list)
        {
            var data = new GimmickMasterData();

            data.id = item.id;

            data.triggerSetId = item.triggerSetId;
            data.productType = (GameMain.Gimmick.Product.Type)item.productType;

            _data[data.id] = data;
        }

        return _data;
    }

    override public IEnumerable<GimmickMasterData> GetAll()
    {
        return GetAllData().Values;
    }

    override public GimmickMasterData Get(int id)
    {
        return base.Get(id);
    }

    public class gimmick_data
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        public int triggerSetId { get; set; }
        public int productType { get; set; }
    }
}
