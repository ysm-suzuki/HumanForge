using System.Collections.Generic;

using UnityMVC;

namespace GameMain
{
    public class GimmickAgent
    {
        private List<Gimmick> _gimmicks = new List<Gimmick>();
        private Dictionary<Gimmick.Product.Type, System.Action> _productDelegates;

        public void AddGimmick(Gimmick gimmick, Board board)
        {
            gimmick.OnTrrigered += () =>
            {
                var type = gimmick.product.type;
                if (_productDelegates.ContainsKey(type))
                    return;
                _productDelegates[type]();
            };

            _gimmicks.Add(gimmick);
        }
        
        public void Notify(Gimmick.Trigger trriger)
        {
            foreach (var gimmick in _gimmicks)
                gimmick.Check(trriger);
        }

        public void RegisterProductType(Gimmick.Product.Type type, System.Action action)
        {
            _productDelegates[type] = action;
        }
    }
}