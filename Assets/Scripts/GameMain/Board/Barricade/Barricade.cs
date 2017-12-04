using System.Collections.Generic;

using UnityMVC;

namespace GameMain
{
    public class Barricade : FieldObject
    {
        private BarricadeData _data;
        private IndividualAttribute _individualAttribute = null;

        public Barricade(BarricadeData data)
        {
            _data = data;

            life = maxLife = _data.life;

            shapePoints = new List<Position>();
            foreach (var position in _data.shapePoints)
                shapePoints.Add(position.Clone());
            shapePoints.ForEach(
                position =>
                {
                    position.x *= _data.sizeRadius;
                    position.y *= _data.sizeRadius;
                });

            _individualAttribute = new IndividualAttribute();
        }

        public override void Tick(float delta)
        {

        }

        
        public override void Damage(Unit attacker, float damage)
        {
            var newDamage = damage - defence;

            if (newDamage < 0)
                newDamage = 0;
            
            base.Damage(attacker, newDamage);
        }

        // ======================================= accessors

        public float defence
        {
            get { return _data.defence; }
        }
        override public float sizeRadius
        {
            get { return _data.sizeRadius; }
        }


        public bool isOwnedBarricade
        {
            get { return _individualAttribute.isOwned; }
            set { _individualAttribute.isOwned = value; }
        }
    }
}