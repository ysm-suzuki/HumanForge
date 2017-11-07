using System.Collections.Generic;

using UnityMVC;

namespace GameMain
{
    public class Wall : FieldObject
    {
        public Wall()
        {
            shapePoints = new List<Position>
            {
                Position.Create(-10, 10),
                Position.Create(10, 10),
                Position.Create(10, -10),
                Position.Create(-10, -10)
            };
        }
    }
}