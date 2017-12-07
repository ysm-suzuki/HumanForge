using System.Collections.Generic;

using UnityMVC;
using GameMain;

public class FaceMasterData
{
    public static FaceDataLoader loader
    {
        get { return FaceDataLoader.Instance; }
    }


    public int id;
    public int type;

    public float generateRed;
    public float generateGreen;
    public float generateBlue;

    public float requireRed;
    public float requireGreen;
    public float requireBlue;

    public int buffSetId;


    public FaceData ToFaceData()
    {
        var buffs = new List<Buff>();
        foreach(var buffData in BuffSetMasterData.loader.GetList(buffSetId))
        {
            var buff = BuffMasterData.loader.Get(buffData.buffMasterId).ToBuff();
            buff.parameter *= buffData.parameterRatio;
            buff.duration.ratio = buffData.durationRatio;
            buffs.Add(buff);
        }

        return new FaceData
        {
            type = (FaceData.Type)type
        };
    }

    public FaceMold ToFaceMold()
    {
        return new FaceMold(
            ToFaceData(),
            new Dictionary<ManaData.Type, float>
            {
                { ManaData.Type.Red, requireRed},
                { ManaData.Type.Green, requireGreen},
                { ManaData.Type.Blue, requireBlue},
            });
    }
}