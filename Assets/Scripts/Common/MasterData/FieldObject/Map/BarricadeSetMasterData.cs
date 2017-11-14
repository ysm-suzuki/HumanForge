using System.Collections.Generic;

using UnityMVC;
using GameMain;

public class BarricadeSetMasterData
{
    public static BarricadeSetMasterDataLoader loader
    {
        get { return BarricadeSetMasterDataLoader.Instance; }
    }
    
    public int id;

    public int barricadeId;
    public float x;
    public float y;
    public bool isTerminal;
}