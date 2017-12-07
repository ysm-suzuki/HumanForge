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

            foreach(var pair in _data.powers)
            {
                var power = FacePower.Create(pair.Key, pair.Value);
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
    }
}