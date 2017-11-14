using System.Collections.Generic;

using UnityMVC;
using GameMain;

public class BarricadeMasterData
{
    public static BarricadeMasterDataLoader loader
    {
        get { return BarricadeMasterDataLoader.Instance; }
    }


    public int id;

    public float life;
    public float defence;

    public float sizeRadius;
    public int shapePointsId;


    public BarricadeData ToBarricadeData()
    {
        const float PI = UnityEngine.Mathf.PI;
        return new BarricadeData
        {
            life = life,
            defence = defence,
            sizeRadius = sizeRadius,
            shapePoints = ShapeMasterData.loader.Get(shapePointsId).positions,
        };
    }
}