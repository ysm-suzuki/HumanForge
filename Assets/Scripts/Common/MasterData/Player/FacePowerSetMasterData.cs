using System.Collections.Generic;

using UnityMVC;
using GameMain;

public class FacePowerSetMasterData
{
    public static FacePowerSetDataLoader loader
    {
        get { return FacePowerSetDataLoader.Instance; }
    }


    public int id;
    public int setId;

    public FacePower.Type type;
    public float value;
}