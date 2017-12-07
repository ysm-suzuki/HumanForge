using System.Collections.Generic;

namespace GameMain
{
    public class FacePowerMana : FacePower
    {
        public ManaData.Type manaType;

        override public void Activate(Player player)
        {
            player.GainMana(manaType, value);

            base.Activate(player);
        }
    }
}