using System.Collections.Generic;

namespace GameMain
{
    public class FacePowerMoveSpeed : FacePower
    {
        override public void Activate(Player player)
        {
            player.PowerUp(new Unit.Aura
            {
                moveSpeed = value
            });
            base.Activate(player);
        }
    }
}