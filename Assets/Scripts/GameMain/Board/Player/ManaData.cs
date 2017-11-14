using System.Collections.Generic;

namespace GameMain
{
    public class ManaData
    {
        public enum Type
        {
            Red,
            Green,
            Blue,
        }

        public Type type;
        public float max;
    }
}