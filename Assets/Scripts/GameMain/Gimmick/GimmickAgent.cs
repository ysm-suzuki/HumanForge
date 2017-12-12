using System.Collections.Generic;

using UnityMVC;

namespace GameMain
{
    public class GimmickAgent
    {
        private List<Gimmick> _gimmicks = new List<Gimmick>();
        private Dictionary<Gimmick.Product.Type, System.Action<Gimmick.Product>> _productDelegates 
            = new Dictionary<Gimmick.Product.Type, System.Action<Gimmick.Product>>();

        public GimmickAgent(Board board)
        {
            RegisterProductType(
                Gimmick.Product.Type.PlaceInitialEnemies,
                product =>
                {
                    board.AddInitialEnemies();
                });
            RegisterProductType(
                Gimmick.Product.Type.StartEnemiesRaid,
                product =>
                {
                    var enemies = board.map.enemyUnits;
                    foreach (var enemy in enemies)
                        if (enemy.groupeId == product.groupeId)
                        enemy.Attack(new List<FieldObject>
                            {
                                board.map.playerUnit
                            });
                });
        }

        public void SetLevel(int level)
        {
            var levelData = LevelMasterData.loader.Get(level);

            foreach (var gimmick in levelData.gimmicks)
                AddGimmick(gimmick);
        }

        public void AddGimmick(Gimmick gimmick)
        {
            gimmick.OnTrrigered += () =>
            {
                var type = gimmick.product.type;

                UnityEngine.Debug.Assert(
                    _productDelegates.ContainsKey(type)
                    , "Triggered the unregistered gimmick");

                _productDelegates[type](gimmick.product);
            };

            _gimmicks.Add(gimmick);
        }
        
        public void Notify(Gimmick.Trigger trriger)
        {
            foreach (var gimmick in _gimmicks)
                gimmick.Check(trriger);
        }


        private void RegisterProductType(Gimmick.Product.Type type, System.Action<Gimmick.Product> action)
        {
            _productDelegates[type] = action;
        }
    }
}