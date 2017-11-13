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
            // kari
            shapePoints = new List<Position>
                {
                    Position.Create(
                        UnityEngine.Mathf.Cos((22.5f + 45 * 0) * PI / 180),
                        UnityEngine.Mathf.Sin((22.5f + 45 * 0) * PI / 180)),
                    Position.Create(
                        UnityEngine.Mathf.Cos((22.5f + 45 * -1) * PI / 180),
                        UnityEngine.Mathf.Sin((22.5f + 45 * -1) * PI / 180)),
                    Position.Create(
                        UnityEngine.Mathf.Cos((22.5f + 45 * -2) * PI / 180),
                        UnityEngine.Mathf.Sin((22.5f + 45 * -2) * PI / 180)),
                    Position.Create(
                        UnityEngine.Mathf.Cos((22.5f + 45 * -3) * PI / 180),
                        UnityEngine.Mathf.Sin((22.5f + 45 * -3) * PI / 180)),
                    Position.Create(
                        UnityEngine.Mathf.Cos((22.5f + 45 * -4) * PI / 180),
                        UnityEngine.Mathf.Sin((22.5f + 45 * -4) * PI / 180)),
                    Position.Create(
                        UnityEngine.Mathf.Cos((22.5f + 45 * -5) * PI / 180),
                        UnityEngine.Mathf.Sin((22.5f + 45 * -5) * PI / 180)),
                    Position.Create(
                        UnityEngine.Mathf.Cos((22.5f + 45 * -6) * PI / 180),
                        UnityEngine.Mathf.Sin((22.5f + 45 * -6) * PI / 180)),
                    Position.Create(
                        UnityEngine.Mathf.Cos((22.5f + 45 * -7) * PI / 180),
                        UnityEngine.Mathf.Sin((22.5f + 45 * -7) * PI / 180)),
                },
        };
    }
}