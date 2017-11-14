using System.Collections.Generic;

using UnityMVC;

namespace GameMain
{
    public class Wall : FieldObject
    {
        private WallData _data;

        public Wall(WallData data)
        {
            _data = data;

            shapePoints = ShapeMasterData.loader.Get(data.shapeId).positions;
            position = data.initialPostion;
        }
    }
}