using System.Collections.Generic;

public class UnitMasterData
{
    public static UnitMasterDataLoader loader
    {
        get { return UnitMasterDataLoader.Instance; }
    }


    public int id;
    
}