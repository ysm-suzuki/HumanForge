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
            shapePoints.AddRange(_data.shapePoints);
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



        // ======================================= accessors
        
        public float defence
        {
            get { return _data.defence; }
        }
        public float sizeRadius
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