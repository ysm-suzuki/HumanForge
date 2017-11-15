using System.Collections.Generic;

namespace GameMain
{
    public class FaceData
    {
        public Dictionary<ManaData.Type, float> manaGenerators = new Dictionary<ManaData.Type, float>();

        public List<Buff> buffs = new List<Buff>();
    }
}