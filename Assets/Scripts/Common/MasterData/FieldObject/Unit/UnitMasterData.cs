using System.Collections.Generic;

using UnityMVC;
using GameMain;

public class UnitMasterData
{
    public static UnitMasterDataLoader loader
    {
        get { return UnitMasterDataLoader.Instance; }
    }

    public enum BuildType
    {
        Never = 0,
        Buildable,
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

    public BuildType buildType = BuildType.Never;
    public float requireRed;
    public float requireGreen;
    public float requireBlue;


    public bool isBuildable
    {
        get
        {
            return buildType == BuildType.Buildable;
        }
    }


    public UnitData ToUnitData()
    {
        return new UnitData
        {
            life = life,
            attack = attack,
            defence = defence,
            moveSpeed = moveSpeed,
            sightRange = sightRange,
            sizeRadius = sizeRadius,
            shapePoints = ShapeMasterData.loader.Get(shapePointsId).positions,

            // kari
            attackActions = new List<UnitAttackData>
                {
                    new UnitAttackData
                    {
                        power = 1,
                        range = 10,
                        warmUpSeconds = 0.1f,
                        coolDownSeconds = 0.5f
                    }
                },
        };
    }

    public UnitMold ToUnitMold()
    {
        return new UnitMold(
            ToUnitData(),
            new Dictionary<ManaData.Type, float>
            {
                { ManaData.Type.Red, requireRed},
                { ManaData.Type.Green, requireGreen},
                { ManaData.Type.Blue, requireBlue}
            });
    }
}