using System.Collections.Generic;

using UnityMVC;
using GameMain;

public class GimmickSetMasterData
{
    public static GimmickSetMasterDataLoader loader
    {
        get { return GimmickSetMasterDataLoader.Instance; }
    }


    public int id;

    public int setId;
    public int gimmickId;


    public Gimmick ToGimmick()
    {
        return GimmickMasterData.loader
                    .Get(gimmickId)
                    .ToGimmick();
    }
}