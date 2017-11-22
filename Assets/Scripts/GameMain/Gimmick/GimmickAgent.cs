using System.Collections.Generic;

using UnityMVC;

namespace GameMain
{
    public class GimmickAgent
    {
        private List<Gimmick> _gimmicks = new List<Gimmick>();
        private Dictionary<Gimmick.Product.Type, System.Action> _productDelegates;

        public GimmickAgent(Board board)
        {
            RegisterProductType(
                Gimmick.Product.Type.PlaceInitialEnemies,
                () =>
                {
                    board.AddInitialEnemies();
                });
            RegisterProductType(
                Gimmick.Product.Type.StartAllEnemiesRaid,
                () =>
                {
                    var enemies = board.map.enemyUnits;
                    foreach (var enemy in enemies)
                        enemy.Attack(new List<FieldObject>
                            {
                                board.map.playerUnit
                            });
                });
        }

        public void SetLevel(int level)
        {
            // kari
            var allGimmicks = GimmickMasterData.loader.GetAll();

            foreach (var gimmickData in allGimmicks)
                AddGimmick(
                    gimmickData.ToGimmick());
        }

        public void AddGimmick(Gimmick gimmick)
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


        private void RegisterProductType(Gimmick.Product.Type type, System.Action action)
        {
            _productDelegates[type] = action;
        }
    }
}