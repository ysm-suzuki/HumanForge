using System.Collections.Generic;

using SQLite.Attribute;

using UnityMVC;

public class BuffDataLoader : MasterDataLoader<BuffMasterData>
{
    private static BuffDataLoader _instance = null;
    public static BuffDataLoader Instance
    {
        get { return _instance ?? new BuffDataLoader(); }
    }
    private BuffDataLoader() { }

    override public Dictionary<int, BuffMasterData> GetAllData()
    {
        if (_data != null)
            return _data;
        
        _data = new Dictionary<int, BuffMasterData>();

        var sqLite = GameMain.SQLite.Instance.connection;
        var list = sqLite.Table<buff_data>();
        foreach (var item in list)
        {
            var data = new BuffMasterData();

            data.id = item.id;

            data.buffId = item.buffId;
            data.name = item.name;

            data.life = item.life;
            data.attack = item.attack;
            data.defense = item.defense;
            data.moveSpeed = item.moveSpeed;
            data.sightRange = item.sightRange;
            data.attackPower = item.attackPower;
            data.attackRange = item.attackRange;
            data.attackCoolDownSeconds = item.attackCoolDownSeconds;
            data.durationType = item.durationType;
            data.durationValue = item.durationValue;
            
            _data[data.id] = data;
        }

        return _data;
    }

    override public IEnumerable<BuffMasterData> GetAll()
    {
        return GetAllData().Values;
    }

    override public BuffMasterData Get(int id)
    {
        return base.Get(id);
    }

    public class buff_data
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        public int buffId { get; set; }
        public string name { get; set; }

        public float life { get; set; }
        public float attack { get; set; }
        public float defense { get; set; }
        public float moveSpeed { get; set; }
        public float sightRange { get; set; }

        public float attackPower { get; set; }
        public float attackRange { get; set; }
        public float attackCoolDownSeconds { get; set; }

        public int durationType { get; set; }
        public float durationValue { get; set; }
    }
}
