using System.Collections.Generic;

using UnityMVC;

namespace GameMain
{
    public class Unit : FieldObject
    {
        private static int s_serialCounter = 0;
        private int _serialId = -1;


        private UnitData _data;

        private Recognition _recognition = null;
        private IndividualAttribute _individualAttribute = null;

        private UnitTaskAgent _taskAgent = null;

        private int _attackActionIndex = 0;
        

        public Unit(UnitData data)
        {
            _serialId = ++s_serialCounter;

            _data = data;

            life = maxLife = _data.life;

            shapePoints = new List<Position>();
            shapePoints.AddRange(_data.shapePoints);
            shapePoints.ForEach(
                position =>
                {
                    position.x *= _data.sizeRadius;
                    position.y *= _data.sizeRadius;
                });
            
            _recognition = new Recognition(this);
            _individualAttribute = new IndividualAttribute();
            _taskAgent = new UnitTaskAgent(this);
        }



        override public void Tick(float delta)
        {
            base.Tick(delta);

            _taskAgent.Tick(delta);
        }


        // ===================================== actions

        public void MoveTo(Position destination)
        {
            _taskAgent.MoveTo(destination);
        }

        public void Attack(List<FieldObject> targets)
        {
            _taskAgent.Attack(targets);
        }


        // =================================== conditions

        public bool IsInSight(FieldObject target)
        {
            var distance = (target.position - position).ToVector().GetLength();
            return distance <= sightRange;
        }


        public bool IsSame(Unit unit)
        {
            return unit.serialId == serialId;
        }


        // ======================================= accessors

        public int serialId
        {
            get { return _serialId; }
        }

        public float attack
        {
            get { return _data.attack + attackAction.power; }
        }
        public float attackRange
        {
            get { return attackAction.range; }
        }
        public float attackWarmUpSeconds
        {
            get { return attackAction.warmUpSeconds; }
        }
        public float attackCoolDownSeconds
        {
            get { return attackAction.coolDownSeconds; }
        }
        public float defence
        {
            get { return _data.defence; }
        }
        public float moveSpeed
        {
            get { return _data.moveSpeed; }
        }

        public float sizeRadius
        {
            get { return _data.sizeRadius; }
        }
        public float sightRange
        {
            get { return _data.sightRange; }
        }


        public Recognition recognition
        {
            get { return _recognition; }
        }
        
        public UnitAttackData attackAction
        {
            get
            {
                return _data.attackActions[_attackActionIndex];
            }
        }


        public bool isPlayerUnit
        {
            get { return _individualAttribute.isPlayerUnit; }
            set { _individualAttribute.isPlayerUnit = value; }
        }
        public bool isOwnedUnit
        {
            get { return _individualAttribute.isOwned; }
            set { _individualAttribute.isOwned = value; }
        }
    }
}