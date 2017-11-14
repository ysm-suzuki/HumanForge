using System.Collections.Generic;

using UnityMVC;
using GameMain;

public class MapMasterData
{
    public static MapMasterDataLoader loader
    {
        get { return MapMasterDataLoader.Instance; }
    }
    
    public int id;

    public float width;
    public float height;
    public List<WallData> walls;
}