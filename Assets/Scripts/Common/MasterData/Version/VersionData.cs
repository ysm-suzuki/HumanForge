using System.Collections.Generic;

using UnityMVC;

public class VersionData
{
    public static VersionDataLoader loader
    {
        get { return VersionDataLoader.Instance; }
    }

    public int major;
    public int minor;
    public int revision;

    public override string ToString()
    {
        return "v" + major + "." + minor + "." + revision;
    }
}