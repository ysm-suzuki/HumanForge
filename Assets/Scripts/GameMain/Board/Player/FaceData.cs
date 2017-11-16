using System.Collections.Generic;

namespace GameMain
{
    public class FaceData
    {
        public enum Type
        {
            Normal = 0, // can forge normally
            Special,
            Initial
        }
        public Type type = Type.Normal;

        public Dictionary<ManaData.Type, float> manaGenerators = new Dictionary<ManaData.Type, float>();

        public List<Buff> buffs = new List<Buff>();
    }
}