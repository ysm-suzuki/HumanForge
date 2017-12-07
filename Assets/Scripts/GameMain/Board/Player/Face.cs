using System.Collections.Generic;

namespace GameMain
{
    public class Face
    {
        public delegate void FacePowerEventHandler(FacePower power);
        public event FacePowerEventHandler OnFacePowerActivated;


        private FaceData _data;

        private List<FacePower> _powers = new List<FacePower>();

        public Face(FaceData data)
        {
            _data = data;

            // kari
            {
                var power = FacePower.Create(FacePower.Type.ManaRed, 1);
                power.OnActivated += () =>
                {
                    if (OnFacePowerActivated != null)
                        OnFacePowerActivated(power);
                };
                _powers.Add(power);
            }
        }


        public void Activate(Player player)
        {
            foreach (var power in _powers)
                power.Activate(player);
        }


        public Dictionary<ManaData.Type, float> manaGenerators
        {
            get
            {
                return _data.manaGenerators;
            }
        }

        public List<Buff> buffs
        {
            get
            {
                return _data.buffs;
            }
        }
    }
}