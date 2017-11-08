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
            _moveTo.OnFinished += () =>
            {
                _moveTo = _moveTo.next;
            };
        }

        public void Attack(List<FieldObject> targets)
        {
            _moveTo = null;

            float attackPower = _owner.attack;
            float attackRange = 50;

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


            float warmUpSeconds = 0.1f;
            float coolDownSeconds = 1.0f;

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

                    target.life -= attackPower;
                }
            };
            _attackTask.OnFinished += () =>
            {
                _attackTask = null;
            };
        }
    }
}