using System.Collections.Generic;

using UnityMVC;
using GameMain;

public class BuffSetMasterData
{
    public static BuffSetDataLoader loader
    {
        get { return BuffSetDataLoader.Instance; }
    }


    public int id;
    public int setId;
    public int buffMasterId;

    public float parameterRatio;
    public float durationRatio;
}