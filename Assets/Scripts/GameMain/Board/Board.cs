using System.Collections.Generic;

using UnityMVC;

namespace GameMain
{
    public class Board
    {
        private Map _map = new Map();


        public void Tick(float delta)
        {
            _map.Tick(delta);
        }
        

        public Map map
        {
            get { return _map; }
        }
    }
}