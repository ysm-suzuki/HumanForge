using System.Collections.Generic;

namespace GameMain
{
    public class FacePowerHeal : FacePower
    {
        override public void Activate(Player player)
        {
            player.life += value;
            base.Activate(player);
        }
    }
}