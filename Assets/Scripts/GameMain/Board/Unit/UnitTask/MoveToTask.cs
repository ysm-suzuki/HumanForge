using UnityMVC;

namespace GameMain
{
    public class MoveToTask : UnitTask
    {
        private Position _destination;
        private MoveToTask _next = null;

        public MoveToTask(Position destination, Unit owner)
        {
            _destination = destination;
            _owner = owner;
        }

        public void RegisterNext(MoveToTask next)
        {
            _next = next;
        }

        override public void Tick(float delta)
        {
            if (_isFinished)
                return;

            if (_destination.FuzzyEquals(_owner.position, 1.0f))
            {
                _owner.position = _destination;
                End();
            }
            else
            {
                _owner.velocity = GetPositionDelta(delta);
            }
        }

        private Position GetPositionDelta(float delta)
        {
            var positionDelta = Position.Create(0, 0);

            positionDelta += Position.Create
                (
                    normalizedDirection.x * _owner.moveSpeed * delta,
                    normalizedDirection.y * _owner.moveSpeed * delta
                ) ;

            return positionDelta;
        }


        public MoveToTask next
        {
            get { return _next; }
        }


        private Position normalizedDirection
        {
            get
            {
                var direction = _destination - _owner.position;
                var length = UnityEngine.Mathf.Sqrt(direction.x * direction.x + direction.y * direction.y);
                return Position.Create(direction.x / length, direction.y / length);
            }
        }
    }
}