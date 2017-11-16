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
        return new FaceData
        {
            type = (FaceData.Type)type,
            manaGenerators = new Dictionary<ManaData.Type, float>
            {
                { ManaData.Type.Red, generateRed},
                { ManaData.Type.Green, generateGreen},
                { ManaData.Type.Blue, generateBlue},
            },

            // kari
            buffs = new List<Buff>
                {
                    new Buff
                    {
                        id = 1,
                        parameter = new Buff.Parameter
                        {
                            life = 5
                        }
                    }
                }
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