using UnityMVC;

namespace GameMain
{
    public class Unit : FieldObject
    {
        public event EventHandler OnDead;


        private static int s_serialCounter = 0;
        private int _serialId = -1;


        private UnitData _data;

        public Unit(UnitData data)
        {
            _serialId = ++s_serialCounter;

            _data = data;

            life = maxLife = _data.life;

            base.OnLifeUpdated += () => 
            {
                if (!isAlive)
                    if (OnDead != null)
                        OnDead();
            };
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
    }
}