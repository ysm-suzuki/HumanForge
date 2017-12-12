using UnityMVC;

namespace GameMain
{
    public class AttackMoveTask : UnitTask
    {
        private FieldObject _target;

        public AttackMoveTask(FieldObject target, Unit owner)
        {
            _target = target;
            _owner = owner;
        }

        override public void Tick(float delta)
        {
            if (_isFinished)
                return;

            base.Tick(delta);

            if (!_target.isAlive)
            {
                Cancel();
                return;
            }

            var destination = _target.position;

            if (destination.FuzzyEquals(_owner.position, 1.0f))
            {
                _owner.position = destination;
                End();
            }
            else
            {
                _owner.velocity = GetPositionDelta(delta);
            }
        }
        
        public Position GetPositionDelta(float delta)
        {
            var positionDelta = Position.Create(0, 0);

            positionDelta += Position.Create
                (
                    normalizedDirection.x * _owner.moveSpeed * delta,
                    normalizedDirection.y * _owner.moveSpeed * delta
                ) ;

            return positionDelta;
        }



        private Position normalizedDirection
        {
            get
            {
                var destination = _target.position;
                var direction = destination - _owner.position;
                var length = UnityEngine.Mathf.Sqrt(direction.x * direction.x + direction.y * direction.y);
                return Position.Create(direction.x / length, direction.y / length);
            }
        }
    }
}