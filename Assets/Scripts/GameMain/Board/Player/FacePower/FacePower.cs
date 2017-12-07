using System.Collections.Generic;

namespace GameMain
{
    public class FacePower
    {
        public delegate void EventHandler();
        public event EventHandler OnActivated;
        

        public enum Type
        {
            None = 0,
            ManaRed,
            MaxManaRed,
            ManaGreen,
            MaxManaGreen,
            ManaBlue,
            MaxManaBlue,
            Heal,
            MaxLife,
            AttackPower,
            AttackRange,
            AttackCoolDownReduction,
            Defense,
            MoveSpeed
        }

        public static FacePower Create(Type type, float value)
        {
            switch (type)
            {
                case Type.ManaRed:
                    return new FacePowerMana
                    {
                        manaType = ManaData.Type.Red,
                        value = value
                    };
                case Type.ManaGreen:
                    return new FacePowerMana
                    {
                        manaType = ManaData.Type.Green,
                        value = value
                    };
                case Type.ManaBlue:
                    return new FacePowerMana
                    {
                        manaType = ManaData.Type.Blue,
                        value = value
                    };
            }

            return null;
        }


        public float value;


        virtual public void Activate(Player player)
        {
            if (OnActivated != null)
                OnActivated();
        }
    }
}