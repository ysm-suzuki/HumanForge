using System.Collections.Generic;

using UnityMVC;
using GameMain;

public class WallSetMasterData
{
    public static WallSetMasterDataLoader loader
    {
        get { return WallSetMasterDataLoader.Instance; }
    }

    public int id;
    public int setId;

    public int shapeId;
    public float x;
    public float y;
    public float rotationTheta;
}