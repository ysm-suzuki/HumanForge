namespace GameMain
{
    public class IndividualAttribute
    {
        enum Attribute
        {
            PlayerUnit  = 1 << 0,
            Owned       = 1 << 1,
            Boss        = 1 << 2,
        }
        private int _bitFlag = 0;

        public bool isPlayerUnit
        {
            get
            {
                return (_bitFlag & (int)Attribute.PlayerUnit) != 0;
            }
            set
            {
                if (value)
                    _bitFlag |= (int)Attribute.PlayerUnit;
                else
                    _bitFlag &= ~(int)Attribute.PlayerUnit;
            }
        }
        public bool isOwned
        {
            get
            {
                return (_bitFlag & (int)Attribute.Owned) != 0;
            }
            set
            {
                if (value)
                    _bitFlag |= (int)Attribute.Owned;
                else
                    _bitFlag &= ~(int)Attribute.Owned;
            }
        }
    }
}