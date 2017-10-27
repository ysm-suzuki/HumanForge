using UnityMVC;

namespace GameMain
{
    public class FieldObject : Model
    {
        public event EventHandler OnLifeUpdated;
        public event EventHandler OnDead;

        protected float _maxLife = 0;
        protected float _life = 0;


        public bool isAlive
        {
            get { return life > 0; }
        }



        public float maxLife
        {
            get { return _maxLife; }
            set
            {
                _maxLife = value;

                if (life > _maxLife)
                    life = _maxLife;
            }
        }

        public float life
        {
            get { return _life; }
            set
            {
                float newLife = value;
                if (newLife > maxLife)
                    newLife = maxLife;

                if (_life == newLife)
                    return;


                _life = newLife;

                if (OnLifeUpdated != null)
                    OnLifeUpdated();
                if (!isAlive
                    && OnDead != null)
                    OnDead();
            }
        }
    }
}