using System.Collections.Generic;

using SQLite.Attribute;

using UnityMVC;

public class GimmickTriggerMasterDataLoader : MasterDataLoader<GimmickTriggerMasterData>
{
    private static GimmickTriggerMasterDataLoader _instance = null;
    public static GimmickTriggerMasterDataLoader Instance
    {
        get { return _instance ?? new GimmickTriggerMasterDataLoader(); }
    }
    private GimmickTriggerMasterDataLoader() { }

    override public Dictionary<int, GimmickTriggerMasterData> GetAllData()
    {
        if (_data != null)
            return _data;
        
        _data = new Dictionary<int, GimmickTriggerMasterData>();

        var sqLite = GameMain.SQLite.Instance.connection;
        var list = sqLite.Table<gimmick_triger_data>();
        foreach (var item in list)
        {
            var data = new GimmickTriggerMasterData();

            data.id = item.id;

            data.type = (GameMain.Gimmick.Trigger.Type)item.type;
            data.value = item.value;

            _data[data.id] = data;
        }

        return _data;
    }

    override public IEnumerable<GimmickTriggerMasterData> GetAll()
    {
        return GetAllData().Values;
    }

    override public GimmickTriggerMasterData Get(int id)
    {
        return base.Get(id);
    }

    public class gimmick_triger_data
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        public int type { get; set; }
        public float value { get; set; }
    }
}
