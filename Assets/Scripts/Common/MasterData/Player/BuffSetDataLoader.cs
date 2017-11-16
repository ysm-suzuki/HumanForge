using System.Collections.Generic;

using SQLite.Attribute;

using UnityMVC;

public class BuffSetDataLoader : MasterDataLoader<BuffSetMasterData>
{
    private static BuffSetDataLoader _instance = null;
    public static BuffSetDataLoader Instance
    {
        get { return _instance ?? new BuffSetDataLoader(); }
    }
    private BuffSetDataLoader() { }

    override public Dictionary<int, BuffSetMasterData> GetAllData()
    {
        if (_data != null)
            return _data;
        
        _data = new Dictionary<int, BuffSetMasterData>();

        var sqLite = GameMain.SQLite.Instance.connection;
        var list = sqLite.Table<buff_set_data>();
        foreach (var item in list)
            _data[item.id] = Convert(item);

        return _data;
    }

    override public IEnumerable<BuffSetMasterData> GetAll()
    {
        return GetAllData().Values;
    }

    override public BuffSetMasterData Get(int id)
    {
        return base.Get(id);
    }
    public List<BuffSetMasterData> GetSet(int setId)
    {
        // TODO : cache?

        var sqLite = GameMain.SQLite.Instance.connection;
        var list = sqLite
                    .Table<buff_set_data>()
                    .Where(set => set.setId == setId);

        var setData = new List<BuffSetMasterData>();
        foreach (var item in list)
            setData.Add(Convert(item));

        return setData;
    }

    private BuffSetMasterData Convert(buff_set_data data)
    {
        var convertedData = new BuffSetMasterData();

        convertedData.id = data.id;
        convertedData.setId = data.setId;
        convertedData.buffMasterId = data.buffMasterId;

        convertedData.parameterRatio = data.parameterRatio;
        convertedData.durationRatio = data.durationRatio;

        return convertedData;
    }

    public class buff_set_data
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public int setId { get; set; }
        public int buffMasterId { get; set; }

        public float parameterRatio { get; set; }
        public float durationRatio { get; set; }
    }
}
