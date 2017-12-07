using System.Collections.Generic;

namespace GameMain
{
    public class FacePowerAttackPower : FacePower
    {
        override public void Activate(Player player)
        {
            player.PowerUp(new Unit.Aura
            {
                attackPower = value
            });
            base.Activate(player);
        }
    }
}