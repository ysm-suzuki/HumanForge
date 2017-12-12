using System.Collections.Generic;

using UnityMVC;

namespace GameMain
{
    public class FaceMold : Model
    {
        public event EventHandler OnSelected;
        public event EventHandler OnStatusUpdated;

        private FaceData _data;
        private Dictionary<ManaData.Type, float> _requiringManas;

        private System.Func<Dictionary<ManaData.Type, float>, bool> _hasEnouphMana;

        public FaceMold(
            FaceData data,
            Dictionary<ManaData.Type, float> requiringManas)
        {
            _data = data;
            _requiringManas = requiringManas;
        }

        public void RegisterConditionFunction(
            System.Func<Dictionary<ManaData.Type, float>, bool> hasEnouphMana)
        {
            _hasEnouphMana = hasEnouphMana;
        }


        public void Select()
        {
            if (OnSelected != null)
                OnSelected();
        }

        public void UpdateStatus()
        {
            if (OnStatusUpdated != null)
                OnStatusUpdated();
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

        public string name
        {
            get { return _data.name; }
        }

        public string description
        {
            get { return _data.description; }
        }

        public Dictionary<ManaData.Type, float> requiringManas
        {
            get
            {
                return _requiringManas;
            }
        }

        public bool isAvailable
        {
            get
            {
                return _hasEnouphMana(requiringManas);
            }
        }
    }
}