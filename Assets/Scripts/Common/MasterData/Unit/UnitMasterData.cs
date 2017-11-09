using System.Collections.Generic;

using UnityMVC;
using GameMain;

public class UnitMasterData
{
    public static UnitMasterDataLoader loader
    {
        get { return UnitMasterDataLoader.Instance; }
    }


    public int id;
    public float life;
    public float attack;
    public float defence;
    public float moveSpeed;

    public float sightRange;

    public float sizeRadius;
    public int shapePointsId;

    public List<int> attackActionIds;


    public UnitData ToUnitData()
    {
        const float PI = UnityEngine.Mathf.PI;
        return new UnitData
        {
            life = life,
            attack = attack,
            defence = defence,
            moveSpeed = moveSpeed,
            sightRange = sightRange,
            sizeRadius = sizeRadius,
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
            attackActions = new List<UnitAttackData>
                {
                    new UnitAttackData
                    {
                        power = 1,
                        range = 50,
                        warmUpSeconds = 0.1f,
                        coolDownSeconds = 0.5f
                    }
                }
        };
    }
}