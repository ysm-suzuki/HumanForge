namespace GameMain
{
    public class BuffDuration
    {
        public delegate void EventHandler();
        public event EventHandler OnFinished;

        public enum Type
        {
            Eternal,
            Once,
            Seconds,
        }
        private Type _type = Type.Eternal;

        private float _limitSeconds = 0;
        private float _elasedSeconds = 0;


        private bool _isEnd = false;

        public BuffDuration()
        {
        }
        public BuffDuration(Type type, float limitSeconds = 0)
        {
            _type = type;
            _limitSeconds = limitSeconds;

            _elasedSeconds = 0;
        }


        public void Tick(float delta)
        {
            if (_isEnd)
                return;

            _elasedSeconds += delta;

            if (_type == Type.Seconds
                && _elasedSeconds >= _limitSeconds)
            {
                End();
            }
        }

        public void End()
        {
            _isEnd = true;

            if (OnFinished != null)
                OnFinished();
        }


        public Type type
        {
            get { return _type; }
        }
    }
}