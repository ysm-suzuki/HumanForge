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
            shapePoints = ShapeMasterData.loader.Get(shapePointsId).positions,
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