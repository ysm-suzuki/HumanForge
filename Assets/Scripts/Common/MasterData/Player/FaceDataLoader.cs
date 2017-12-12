using System.Collections.Generic;

using SQLite.Attribute;

using UnityMVC;

public class FaceDataLoader : MasterDataLoader<FaceMasterData>
{
    private static FaceDataLoader _instance = null;
    public static FaceDataLoader Instance
    {
        get { return _instance ?? new FaceDataLoader(); }
    }
    private FaceDataLoader() { }

    override public Dictionary<int, FaceMasterData> GetAllData()
    {
        if (_data != null)
            return _data;
        
        _data = new Dictionary<int, FaceMasterData>();

        var sqLite = GameMain.SQLite.Instance.connection;
        var list = sqLite.Table<face_data>();
        foreach (var item in list)
        {
            var data = new FaceMasterData();

            data.id = item.id;
            data.type = item.type;
            data.name = item.name;

            data.requireRed = item.requireRed;
            data.requireGreen = item.requireGreen;
            data.requireBlue = item.requireBlue;
            data.facePoewrSetId = item.facePoewrSetId;

            _data[data.id] = data;
        }

        return _data;
    }

    override public IEnumerable<FaceMasterData> GetAll()
    {
        return GetAllData().Values;
    }

    override public FaceMasterData Get(int id)
    {
        return base.Get(id);
    }

    public class face_data
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public int type { get; set; }
        public string name { get; set; }

        public float requireRed { get; set; }
        public float requireGreen { get; set; }
        public float requireBlue { get; set; }

        public int facePoewrSetId { get; set; }
    }
}
