using System.Collections.Generic;

using UnityMVC;

namespace GameMain
{
    public class UnitMold : Model
    {
        public event EventHandler OnSelected;

        private UnitData _data;
        private Dictionary<ManaData.Type, float> _requiringManas;

        public UnitMold(
            UnitData data,
            Dictionary<ManaData.Type, float> requiringManas)
        {
            _data = data;
            _requiringManas = requiringManas;
        }


        public void Select()
        {
            if (OnSelected != null)
                OnSelected();
        }

        public Unit Pick()
        {
            return new Unit(_data);
        }

        
        public Dictionary<ManaData.Type, float> requiringManas
        {
            get
            {
                return _requiringManas;
            }
        }
    }
}