using System.Collections.Generic;

namespace GameMain
{
    public class Mana
    {
        public delegate void EventHandler();
        public event EventHandler OnAmountUpdated;


        private ManaData _data;
        private float _amount = 0;
        private float _max = 0;


        public Mana(ManaData data)
        {
            _data = data;
            _max = _data.max;
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

                bool isUpdatedAsInteger = UnityEngine.Mathf.FloorToInt(_amount) != UnityEngine.Mathf.FloorToInt(newAmount);

                _amount = newAmount;

                if (isUpdatedAsInteger)
                    if (OnAmountUpdated != null)
                        OnAmountUpdated();
            }
        }

        public float max
        {
            get
            {
                return _max;
            }
            set
            {
                _max = value;
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