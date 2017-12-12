using System.Collections.Generic;

namespace GameMain
{
    public class FacePowerAttackCoolDownReduction : FacePower
    {
        override public void Activate(Player player)
        {
            player.PowerUp(new Unit.Aura
            {
                attackCoolDownReductioin = value
            });
            base.Activate(player);
        }
    }
}