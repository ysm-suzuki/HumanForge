using System.Collections.Generic;

namespace GameMain
{
    public class FacePowerDefense : FacePower
    {
        override public void Activate(Player player)
        {
            player.PowerUp(new Unit.Aura
            {
                defense = value
            });
            base.Activate(player);
        }
    }
}