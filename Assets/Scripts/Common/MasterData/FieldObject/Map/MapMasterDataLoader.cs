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

            data.walls = new List<Wall>();
            var wallDataSet = WallSetMasterData.loader.GetList(item.wallSetId);
            foreach (var wallData in wallDataSet)
                data.walls.Add(new Wall(new WallData
                {
                    shapeId = wallData.shapeId,
                    shapeRotationTheta = wallData.rotationTheta,
                    initialPostion = Position.Create(wallData.x, wallData.y)
                }));

            data.barricades = new List<Barricade>();
            var barricadeDataSet = BarricadeSetMasterData.loader.GetList(item.barricadeSetId);
            foreach (var barricadeData in barricadeDataSet)
            {
                var barricade = new Barricade(BarricadeMasterData.loader.Get(barricadeData.barricadeId).ToBarricadeData());
                barricade.position = Position.Create(barricadeData.x, barricadeData.y);
                data.barricades.Add(barricade);
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

        public int wallSetId { get; set; }
        public int barricadeSetId { get; set; }
    }
}
