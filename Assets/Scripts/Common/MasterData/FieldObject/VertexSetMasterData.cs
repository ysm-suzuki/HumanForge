using System.Collections.Generic;

using UnityMVC;
using GameMain;

public class VertexSetMasterData
{
    public static VertexSetMasterDataLoader loader
    {
        get { return VertexSetMasterDataLoader.Instance; }
    }

    public int id;
    public float x;
    public float y;

    public Position ToPosition()
    {
        return Position.Create(x, y); 
    }
}