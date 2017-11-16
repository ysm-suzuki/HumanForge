using System.Collections.Generic;

using UnityMVC;
using GameMain;

public class BuffMasterData
{
    public static BuffDataLoader loader
    {
        get { return BuffDataLoader.Instance; }
    }


    public int id;

    public int buffId; // identify sereis of buffs 
    public string name;

    public float life = 0;
    public float attack = 0;
    public float defense = 0;
    public float moveSpeed = 0;
    public float sightRange = 0;

    public float attackPower = 0;
    public float attackRange = 0;
    public float attackCoolDownSeconds = 0;

    public int durationType;
    public float durationValue;

    public Buff ToBuff()
    {
        return new Buff
        {
            id = buffId,
            name = name,
            parameter = new Buff.Parameter
            {
                life = life,
                attack = attack,
                defense = defense,
                moveSpeed = moveSpeed,
                sightRange = sightRange,
                attackPower = attackPower,
                attackRange = attackRange,
                attackCoolDownSeconds = attackCoolDownSeconds,
            },
            duration = new BuffDuration((BuffDuration.Type)durationType, durationValue),
        };
    }
}