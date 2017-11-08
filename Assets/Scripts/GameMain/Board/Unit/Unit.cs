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

        // attributes
        public bool isPlayerUnit = false;
        public bool isOwnedUnit = false;
        //

        // tasks
        private MoveToTask _moveTo = null;
        private AttackTask _attackTask = null;
        private AttackMoveTask _attackMoveTask = null;

        public Unit(UnitData data)
        {
            _serialId = ++s_serialCounter;

            _data = data;

            life = maxLife = _data.life;

            const float PI = UnityEngine.Mathf.PI;
            shapePoints = new List<Position>
            {
                Position.Create(
                    _data.sizeRadius * UnityEngine.Mathf.Cos((22.5f + 45 * 0) * PI / 180),
                    _data.sizeRadius * UnityEngine.Mathf.Sin((22.5f + 45 * 0) * PI / 180)),
                Position.Create(
                    _data.sizeRadius * UnityEngine.Mathf.Cos((22.5f + 45 * -1) * PI / 180),
                    _data.sizeRadius * UnityEngine.Mathf.Sin((22.5f + 45 * -1) * PI / 180)),
                Position.Create(
                    _data.sizeRadius * UnityEngine.Mathf.Cos((22.5f + 45 * -2) * PI / 180),
                    _data.sizeRadius * UnityEngine.Mathf.Sin((22.5f + 45 * -2) * PI / 180)),
                Position.Create(
                    _data.sizeRadius * UnityEngine.Mathf.Cos((22.5f + 45 * -3) * PI / 180),
                    _data.sizeRadius * UnityEngine.Mathf.Sin((22.5f + 45 * -3) * PI / 180)),
                Position.Create(
                    _data.sizeRadius * UnityEngine.Mathf.Cos((22.5f + 45 * -4) * PI / 180),
                    _data.sizeRadius * UnityEngine.Mathf.Sin((22.5f + 45 * -4) * PI / 180)),
                Position.Create(
                    _data.sizeRadius * UnityEngine.Mathf.Cos((22.5f + 45 * -5) * PI / 180),
                    _data.sizeRadius * UnityEngine.Mathf.Sin((22.5f + 45 * -5) * PI / 180)),
                Position.Create(
                    _data.sizeRadius * UnityEngine.Mathf.Cos((22.5f + 45 * -6) * PI / 180),
                    _data.sizeRadius * UnityEngine.Mathf.Sin((22.5f + 45 * -6) * PI / 180)),
                Position.Create(
                    _data.sizeRadius * UnityEngine.Mathf.Cos((22.5f + 45 * -7) * PI / 180),
                    _data.sizeRadius * UnityEngine.Mathf.Sin((22.5f + 45 * -7) * PI / 180)),
            };

            _recognition = new Recognition(this);
        }



        override public void Tick(float delta)
        {
            base.Tick(delta);

            if (_moveTo != null)
                _moveTo.Tick(delta);
            if(_attackMoveTask != null)
                _attackMoveTask.Tick(delta);

            if (_attackTask == null)
            {
                var target = recognition.GetTopPriorityTarget();

                if (target != null)
                    Attack(new List<FieldObject> { target });
            }
            else
            {
                _attackTask.Tick(delta);
            }
        }



        public void MoveTo(Position destination)
        {
            _moveTo = new MoveToTask(destination, this);
            _moveTo.OnFinished += () => 
            {
                _moveTo = _moveTo.next;
            };
        }

        public void Attack(List<FieldObject> targets)
        {
            _moveTo = null;

            float attackPower = attack;
            float attackRange = 50;

            foreach(var target in targets)
            {
                float distance = (target.position - position).ToVector().GetLength();

                if (distance > attackRange)
                {
                    _attackMoveTask = new AttackMoveTask(target, this);
                    _attackMoveTask.OnFinished += () => 
                    {
                        _attackMoveTask = null;
                    };
                    return;
                }
            }

            if (_attackMoveTask != null)
                _attackMoveTask.Cancel();


            float warmUpSeconds = 0.1f;
            float coolDownSeconds = 1.0f;

            _attackTask = new AttackTask(
                                targets, 
                                warmUpSeconds, 
                                coolDownSeconds,
                                this);
            _attackTask.OnAttackFired += () =>
            {
                foreach (var target in targets)
                {
                    if (!target.isAlive)
                        continue;

                    target.life -= attackPower;
                }
            };
            _attackTask.OnFinished += () =>
            {
                _attackTask = null;
            };
        }


        public bool IsInSight(FieldObject target)
        {
            float sightRange = 300;

            var distance = (target.position - position).ToVector().GetLength();

            return distance <= sightRange;
        }


        public bool IsSame(Unit unit)
        {
            return unit.serialId == serialId;
        }

        public int serialId
        {
            get { return _serialId; }
        }

        public float attack
        {
            get { return _data.attack; }
        }
        public float defence
        {
            get { return _data.defence; }
        }
        public float attackSpeed
        {
            get { return _data.attackSpeed; }
        }
        public float moveSpeed
        {
            get { return _data.moveSpeed; }
        }

        public float sizeRadius
        {
            get { return _data.sizeRadius; }
        }


        public Recognition recognition
        {
            get { return _recognition; }
        }
    }
}