using System.Collections.Generic;

using SQLite.Attribute;

using UnityMVC;
using GameMain;

public class MapMasterDataLoader : MasterDataLoader<MapMasterData>
{
    private static MapMasterDataLoader _instance = null;
    public static MapMasterDataLoader Instance
    {
        get { return _instance ?? new MapMasterDataLoader(); }
    }
    private MapMasterDataLoader() { }

    override public Dictionary<int, MapMasterData> GetAllData()
    {
        if (_data != null)
            return _data;
        
        _data = new Dictionary<int, MapMasterData>();

        var sqLite = GameMain.SQLite.Instance.connection;
        var list = sqLite.Table<map_data>();
        foreach (var item in list)
        {
            var data = new MapMasterData();

            data.id = item.id;

            data.width = item.width;
            data.height = item.height;

            data.walls = new List<WallData>();
            int wallSetId = item.wallSetFirstId;
            while (wallSetId > 0)
            {
                var wallSet = WallSetMasterData.loader.Get(wallSetId);

                if (wallSet == null)
                    break;
                
                data.walls.Add(new WallData
                {
                    shapeId = wallSet.shapeId,
                    shapeRotationTheta = wallSet.rotationTheta,
                    initialPostion = Position.Create(wallSet.x, wallSet.y)
                });

                if (wallSet.isTerminal)
                    break;

                wallSetId++;
            }

            _data[data.id] = data;
        }

        return _data;
    }

    override public IEnumerable<MapMasterData> GetAll()
    {
        return GetAllData().Values;
    }

    override public MapMasterData Get(int id)
    {
        return base.Get(id);
    }

    public class map_data
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        public float width { get; set; }
        public float height { get; set; }

        public int wallSetFirstId { get; set; }
    }
}
