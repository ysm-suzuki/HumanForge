﻿using System.Collections.Generic;
using UnityMVC;

namespace GameMain
{
    public class UnitData
    {
        public string name;

        public float life;
        public float attack;
        public float defense;
        public float moveSpeed;

        public float penetrationRate = 0.05f;

        public float sightRange;

        public float sizeRadius;
        public List<Position> shapePoints;

        public List<UnitAttackData> attackActions;

        public int groupeId = -1;
    }
}