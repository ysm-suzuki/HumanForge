using System.Collections.Generic;

namespace GameMain
{
    public class FacePowerMaxLife : FacePower
    {
        override public void Activate(Player player)
        {
            player.PowerUp(new Unit.Aura
            {
                life = value
            });
            base.Activate(player);
        }
    }
}