using UnityMVC;

namespace GameMain
{
    public class MoveTo
    {
        public delegate void EventHandler();
        public event EventHandler OnFinished;

        private Unit _owner = null;
        private Position _destination;
        private MoveTo _next = null;

        private bool _isFinished = false;

        public MoveTo(Position destination, Unit owner)
        {
            _destination = destination;
            _owner = owner;
        }

        public void RegisterNext(MoveTo next)
        {
            _next = next;
        }

        public void Tick()
        {
            if (_isFinished)
                return;

            if (_destination.FuzzyEquals(_owner.position, 0.1f))
            {
                _isFinished = true;
                if (OnFinished != null)
                    OnFinished();
            }
        }

        public Position GetPositionDelta(float delta)
        {
            var positionDelta = Position.Create(0, 0);

            var direction = _destination - _owner.position;
            var length = direction.x * direction.x + direction.y * direction.y;
            var normalizedDirection = Position.Create(direction.x / length, direction.y / length);
            positionDelta += Position.Create
                (
                    normalizedDirection.x * _owner.moveSpeed * delta,
                    normalizedDirection.y * _owner.moveSpeed * delta
                ) ;

            return positionDelta;
        }


        public MoveTo next
        {
            get { return _next; }
        }
    }
}