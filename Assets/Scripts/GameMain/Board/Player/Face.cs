using System.Collections.Generic;

namespace GameMain
{
    public class Face
    {
        private FaceData _data;


        public Face(FaceData data)
        {
            _data = data;
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