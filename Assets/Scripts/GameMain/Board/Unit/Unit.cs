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
        private IndividualAttribute _individualAttribute = new IndividualAttribute();

        private UnitTaskAgent _taskAgent = null;
        private int _attackActionIndex = 0;

        public Unit(UnitData data)
        {
            _serialId = ++s_serialCounter;

            _data = data;

            life = maxLife = _data.life;

            shapePoints = new List<Position>();
            foreach (var position in _data.shapePoints)
                shapePoints.Add(position.Clone());
            shapePoints.ForEach(
                position =>
                {
                    position.x *= _data.sizeRadius;
                    position.y *= _data.sizeRadius;
                });
            
            _recognition = new Recognition(this);
            _taskAgent = new UnitTaskAgent(this);
        }


        public void SetIndividualAttribute(int flags)
        {
            _individualAttribute.Set(flags);
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
            get
            {
                float buffValue = 0;
                foreach(var buff in _buffs)
                {
                    buffValue += buff.parameter.attack;

                    if (buff.isOnce)
                        buff.duration.End();
                }

                return _data.attack + 
                        attackAction.power +
                        buffValue;
            }
        }
        public float attackRange
        {
            get
            {
                float buffValue = 0;
                foreach (var buff in _buffs)
                {
                    buffValue += buff.parameter.attackRange;

                    if (buff.isOnce)
                        buff.duration.End();
                }

                return attackAction.range +
                        buffValue;
            }
        }
        public float attackWarmUpSeconds
        {
            get { return attackAction.warmUpSeconds; }
        }
        public float attackCoolDownSeconds
        {
            get
            {
                float buffValue = 0;
                foreach (var buff in _buffs)
                {
                    buffValue += buff.parameter.attackCoolDownSeconds;

                    if (buff.isOnce)
                        buff.duration.End();
                }

                return attackAction.coolDownSeconds +
                        buffValue;
            }
        }
        public float defence
        {
            get
            {
                float buffValue = 0;
                foreach (var buff in _buffs)
                {
                    buffValue += buff.parameter.defense;

                    if (buff.isOnce)
                        buff.duration.End();
                }

                return _data.defence +
                        buffValue;
            }
        }
        public float moveSpeed
        {
            get
            {
                float buffValue = 0;
                foreach (var buff in _buffs)
                {
                    buffValue += buff.parameter.moveSpeed;

                    if (buff.isOnce)
                        buff.duration.End();
                }

                return _data.moveSpeed +
                        buffValue;
            }
        }

        public float sizeRadius
        {
            get { return _data.sizeRadius; }
        }
        public float sightRange
        {
            get
            {
                float buffValue = 0;
                foreach (var buff in _buffs)
                {
                    buffValue += buff.parameter.sightRange;

                    if (buff.isOnce)
                        buff.duration.End();
                }

                return _data.sightRange +
                        buffValue;
            }
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
        public bool isBoss
        {
            get { return _individualAttribute.isBoss; }
            set { _individualAttribute.isBoss = value; }
        }
    }
}