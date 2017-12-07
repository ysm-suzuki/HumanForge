using System.Collections.Generic;

namespace GameMain
{
    public class FacePowerMaxMana : FacePower
    {
        public ManaData.Type manaType;

        override public void Activate(Player player)
        {
            player.ExpandMana(manaType, value);

            base.Activate(player);
        }
    }
}