using System.Collections.Generic;

using SQLite.Attribute;

using UnityMVC;

public class FacePowerSetDataLoader : MasterDataLoader<FacePowerSetMasterData>
{
    private static FacePowerSetDataLoader _instance = null;
    public static FacePowerSetDataLoader Instance
    {
        get { return _instance ?? new FacePowerSetDataLoader(); }
    }
    private FacePowerSetDataLoader() { }

    override public Dictionary<int, FacePowerSetMasterData> GetAllData()
    {
        if (_data != null)
            return _data;
        
        _data = new Dictionary<int, FacePowerSetMasterData>();

        var sqLite = GameMain.SQLite.Instance.connection;
        var list = sqLite.Table<face_power_set_data>();
        foreach (var item in list)
            _data[item.id] = Convert(item);

        return _data;
    }

    override public IEnumerable<FacePowerSetMasterData> GetAll()
    {
        return GetAllData().Values;
    }

    override public FacePowerSetMasterData Get(int id)
    {
        return base.Get(id);
    }
    public List<FacePowerSetMasterData> GetList(int setId)
    {
        // TODO : cache?

        var sqLite = GameMain.SQLite.Instance.connection;
        var list = sqLite
                    .Table<face_power_set_data>()
                    .Where(set => set.setId == setId);

        var setData = new List<FacePowerSetMasterData>();
        foreach (var item in list)
            setData.Add(Convert(item));

        return setData;
    }

    private FacePowerSetMasterData Convert(face_power_set_data data)
    {
        var convertedData = new FacePowerSetMasterData();

        convertedData.id = data.id;
        convertedData.setId = data.setId;

        convertedData.type = (GameMain.FacePower.Type)data.type;
        convertedData.value = data.value;

        return convertedData;
    }

    public class face_power_set_data
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public int setId { get; set; }

        public int type { get; set; }
        public float value { get; set; }
    }
}
