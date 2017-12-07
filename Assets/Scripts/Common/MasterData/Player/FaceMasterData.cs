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

    public float requireRed;
    public float requireGreen;
    public float requireBlue;

    public int facePoewrSetId;


    public FaceData ToFaceData()
    {
        var powers = new Dictionary<FacePower.Type, float>();
        var powerData = FacePowerSetMasterData.loader.GetList(facePoewrSetId);

        foreach (var data in powerData)
            powers[data.type] = data.value;

        return new FaceData
        {
            type = (FaceData.Type)type,
            powers = powers
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