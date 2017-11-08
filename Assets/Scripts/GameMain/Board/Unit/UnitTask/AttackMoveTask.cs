using UnityMVC;

namespace GameMain
{
    public class AttackMoveTask
    {
        public delegate void EventHandler();
        public event EventHandler OnFinished;

        private Unit _owner = null;
        private FieldObject _target;

        private bool _isFinished = false;

        public AttackMoveTask(FieldObject target, Unit owner)
        {
            _target = target;
            _owner = owner;
        }

        public void Tick(float delta)
        {
            if (_isFinished)
                return;

            if (!_target.isAlive)
            {
                Cancel();
                return;
            }

            var destination = _target.position;

            if (destination.FuzzyEquals(_owner.position, 1.0f))
            {
                _owner.position = destination;

                _isFinished = true;
                if (OnFinished != null)
                    OnFinished();
            }
            else
            {
                _owner.velocity = GetPositionDelta(delta);
            }
        }

        public void Cancel()
        {
            _isFinished = true;
            if (OnFinished != null)
                OnFinished();
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