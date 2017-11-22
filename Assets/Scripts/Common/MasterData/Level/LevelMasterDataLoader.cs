using System.Collections.Generic;

using SQLite.Attribute;

using UnityMVC;

public class LevelMasterDataLoader : MasterDataLoader<LevelMasterData>
{
    private static LevelMasterDataLoader _instance = null;
    public static LevelMasterDataLoader Instance
    {
        get { return _instance ?? new LevelMasterDataLoader(); }
    }
    private LevelMasterDataLoader() { }

    override public Dictionary<int, LevelMasterData> GetAllData()
    {
        if (_data != null)
            return _data;
        
        _data = new Dictionary<int, LevelMasterData>();

        var sqLite = GameMain.SQLite.Instance.connection;
        var list = sqLite.Table<level_data>();
        foreach (var item in list)
        {
            var data = new LevelMasterData();

            data.id = item.id;

            data.mapId = item.mapId;
            data.unitSetId = item.unitSetId;
            data.gimmickSetId = item.gimmickSetId;
            
            _data[data.id] = data;
        }

        return _data;
    }

    override public IEnumerable<LevelMasterData> GetAll()
    {
        return GetAllData().Values;
    }

    override public LevelMasterData Get(int id)
    {
        return base.Get(id);
    }

    public class level_data
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        public int mapId { get; set; }
        public int unitSetId { get; set; }
        public int gimmickSetId { get; set; }
    }
}
