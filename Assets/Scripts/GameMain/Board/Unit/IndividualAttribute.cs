namespace GameMain
{
    public class IndividualAttribute
    {
        enum Attribute
        {
            PlayerUnit  = 1 << 0,
            OwnedUnit   = 1 << 1,
            BossUnit    = 1 << 2,
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
        public bool isOwnedUnit
        {
            get
            {
                return (_bitFlag & (int)Attribute.OwnedUnit) != 0;
            }
            set
            {
                if (value)
                    _bitFlag |= (int)Attribute.OwnedUnit;
                else
                    _bitFlag &= ~(int)Attribute.OwnedUnit;
            }
        }
    }
}