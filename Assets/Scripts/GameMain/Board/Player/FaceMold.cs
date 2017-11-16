using System.Collections.Generic;

using UnityMVC;

namespace GameMain
{
    public class FaceMold : Model
    {
        public event EventHandler OnSelected;

        private FaceData _data;
        private Dictionary<ManaData.Type, float> _requiringManas;

        public FaceMold(
            FaceData data,
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

        public Face Pick()
        {
            return new Face(_data);
        }



        public FaceData.Type type
        {
            get
            {
                return _data.type;
            }
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