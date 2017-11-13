using System.Collections.Generic;

using UnityMVC;
using GameMain;

public class ShapeMasterData
{
    public static ShapeMasterDataLoader loader
    {
        get { return ShapeMasterDataLoader.Instance; }
    }


    public int id;
    public List<Position> positions;
}