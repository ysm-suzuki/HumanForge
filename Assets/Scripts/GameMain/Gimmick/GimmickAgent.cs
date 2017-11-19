using System.Collections.Generic;

using UnityMVC;

namespace GameMain
{
    public class GimmickAgent
    {
        private List<Gimmick> _gimmicks = new List<Gimmick>();

        public void AddGimmick(Gimmick gimmick)
        {
            _gimmicks.Add(gimmick);
        }
        
        public void Notify(Gimmick.Trigger trriger)
        {
            foreach (var gimmick in _gimmicks)
                gimmick.Check(trriger);
        }
    }
}