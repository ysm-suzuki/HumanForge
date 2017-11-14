using System.Collections.Generic;

namespace GameMain
{
    public class Mana
    {
        public delegate void EventHandler();
        public event EventHandler OnAmountUpdated;


        private ManaData _data;
        private float _amount = 0;


        public Mana(ManaData data)
        {
            _data = data;
        }
        


        public float amount
        {
            get
            {
                return _amount;
            }
            set
            {
                float newAmount = value;

                if (newAmount > max)
                    newAmount = max;
                if (newAmount < 0)
                    newAmount = 0;

                if (_amount == newAmount)
                    return;

                _amount = newAmount;

                if (OnAmountUpdated != null)
                    OnAmountUpdated();
            }
        }

        public float max
        {
            get
            {
                return _data.max;
            }
        }

        public ManaData.Type type
        {
            get
            {
                return _data.type;
            }
        }
    }
}