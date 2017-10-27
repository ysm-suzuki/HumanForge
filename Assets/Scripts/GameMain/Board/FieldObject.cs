using UnityMVC;

namespace GameMain
{
    public class FieldObject : Model
    {
        public event EventHandler OnLifeUpdated;

        private float _life = 0;



        public float life
        {
            get { return _life; }
            set
            {
                if (_life == value)
                    return;

                _life = value;

                if (OnLifeUpdated != null)
                    OnLifeUpdated();
            }
        }
    }
}