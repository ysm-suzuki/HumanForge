using System.Collections.Generic;

using SQLite.Attribute;

using UnityMVC;

public class VersionDataLoader : MasterDataLoader<VersionData>
{
    private static VersionDataLoader _instance = null;
    public static VersionDataLoader Instance
    {
        get { return _instance ?? new VersionDataLoader(); }
    }
    private VersionDataLoader() { }

    public VersionData Get()
    {
        var sqLite = GameMain.SQLite.Instance.connection;
        var list = sqLite.Table<version_data>();
        var data = list.FirstOrDefault();

        return new VersionData
        {
            major = data.major,
            minor = data.minor,
            revision = data.revision,
        };
    }

    public class version_data
    {
        public int major { get; set; }
        public int minor { get; set; }
        public int revision { get; set; }
    }
}
