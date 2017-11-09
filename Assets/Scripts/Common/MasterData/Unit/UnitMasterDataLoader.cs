﻿using System.Collections.Generic;

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

            data.id = item.id;
            data.life = item.life;
            data.attack = item.attack;
            data.defence = item.defence;
            data.moveSpeed = item.moveSpeed;
            data.sightRange = item.sightRange;
            data.sizeRadius = item.sizeRadius;
            data.shapePointsId = item.shapePointsId;

            var csv = item.attackActionIdsCSV.Split(",".ToCharArray());
            var ids = new List<int>();
            for (int i = 0; i < csv.Length; i++)
                ids.Add(System.Int32.Parse(csv[i]));
            data.attackActionIds = ids;

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
        public float life { get; set; }
        public float attack { get; set; }
        public float defence { get; set; }
        public float moveSpeed { get; set; }

        public float sightRange { get; set; }

        public float sizeRadius { get; set; }
        public int shapePointsId { get; set; }

        public string attackActionIdsCSV { get; set; }
    }
}
