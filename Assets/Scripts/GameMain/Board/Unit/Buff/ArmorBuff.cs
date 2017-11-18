namespace GameMain
{
    public class ArmorBuff
    {
        public int id;
        public string name = "No name";
        
        public enum Attribute
        {
            Normal,
            Weak
        }
        public Attribute attribute = Attribute.Normal;
    }
}