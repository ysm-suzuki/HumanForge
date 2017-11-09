using System.Collections.Generic;
using UnityMVC;

namespace GameMain
{
    public class UnitData
    {
        public float life;
        public float attack;
        public float defence;
        public float moveSpeed;

        public float sightRange;

        public float sizeRadius;
        public List<Position> shapePoints;

        public List<UnitAttackData> attackActions;
    }
}