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

    public bool IsUpperFor(int targetMajor, int targetMinor, int targetRevision)
    {
        if (major > targetMajor)
            return true;
        if (major < targetMajor)
            return false;

        if (minor > targetMinor)
            return true;
        if (minor < targetMinor)
            return false;

        return revision > targetRevision;
    }

    public bool IsLowerFor(int targetMajor, int targetMinor, int targetRevision)
    {
        if (major < targetMajor)
            return true;
        if (major > targetMajor)
            return false;

        if (minor < targetMinor)
            return true;
        if (minor > targetMinor)
            return false;

        return revision < targetRevision;
    }
}