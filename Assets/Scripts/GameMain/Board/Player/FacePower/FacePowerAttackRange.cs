using System.Collections.Generic;

namespace GameMain
{
    public class FacePowerAttackRange : FacePower
    {
        override public void Activate(Player player)
        {
            player.PowerUp(new Unit.Aura
            {
                attackRange = value
            });
            base.Activate(player);
        }
    }
}