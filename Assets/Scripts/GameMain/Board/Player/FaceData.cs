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

        public Dictionary<FacePower.Type, float> powers = new Dictionary<FacePower.Type, float>();
    }
}