using System.Collections.Generic;

using UnityMVC;

namespace GameMain
{
    public class UnitTaskAgent
    {
        private MoveToTask _moveTo = null;
        private AttackTask _attackTask = null;
        private AttackMoveTask _attackMoveTask = null;

        private Unit _owner = null;

        public UnitTaskAgent(Unit owner)
        {
            _owner = owner;
        }

        public void Tick(float delta)
        {
            if (_moveTo != null)
                _moveTo.Tick(delta);
            if (_attackMoveTask != null)
                _attackMoveTask.Tick(delta);

            if (_attackTask == null)
            {
                var target = _owner.recognition.GetTopPriorityTarget();

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
            _moveTo = new MoveToTask(destination, _owner);

            if (_attackMoveTask != null)
            {
                _attackMoveTask.Cancel();
                _moveTo.isRetreat = true;
            }
            if (_attackTask != null)
            {
                _attackTask.Cancel();
                _moveTo.isRetreat = true;
            }

            _moveTo.OnFinished += () =>
            {
                _moveTo = _moveTo.next;
            };
        }

        public void Attack(List<FieldObject> targets)
        {
            if (_moveTo != null)
            {
                if (_moveTo.isRetreat)
                    return;

                _moveTo = null;
            }

            float attackPower = _owner.attack;
            float attackRange = _owner.attackRange;

            foreach (var target in targets)
            {
                float distance = (target.position - _owner.position).ToVector().GetLength();

                if (distance > attackRange)
                {
                    _attackMoveTask = new AttackMoveTask(target, _owner);
                    _attackMoveTask.OnFinished += () =>
                    {
                        _attackMoveTask = null;
                    };
                    return;
                }
            }

            if (_attackMoveTask != null)
                _attackMoveTask.Cancel();


            float warmUpSeconds = _owner.attackWarmUpSeconds;
            float coolDownSeconds = _owner.attackCoolDownSeconds;

            _attackTask = new AttackTask(
                                targets,
                                warmUpSeconds,
                                coolDownSeconds,
                                _owner);
            _attackTask.OnAttackFired += () =>
            {
                foreach (var target in targets)
                {
                    if (!target.isAlive)
                        continue;

                    target.Damage(_owner, attackPower);
                }
            };
            _attackTask.OnFinished += () =>
            {
                _attackTask = null;
            };
        }
    }
}