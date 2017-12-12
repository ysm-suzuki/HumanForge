using System.Collections.Generic;

using UnityMVC;

namespace GameMain
{
    public class Gimmick
    {
        public delegate void EventHandler();
        public event EventHandler OnTrrigered;

        public class Trigger
        {
            public enum Type
            {
                None = 0,
                StartMap,
                PassTime,
                TouchEnemyGroupe,
                WipeOutEnemyGroupe,
                BuildUnit,
            }
            public Type type = Type.None;
            public float value = 0;

            public bool IsValid(Trigger trriger)
            {
                if (trriger.type != type)
                    return false;

                if (type == Type.TouchEnemyGroupe
                    || type == Type.WipeOutEnemyGroupe
                    || type == Type.BuildUnit)
                {
                    return trriger.value == value;
                }

                if (type == Type.PassTime)
                {
                    return trriger.value >= value;
                }

                return true;
            }
        }


        public class Product
        {
            public enum Type
            {
                None = 0,
                PlaceInitialEnemies,
                StartEnemiesRaid,
            }
            public Type type = Type.None;
            public int groupeId;
        }

        private List<Trigger> _orCondition;
        private Product _product;


        public Gimmick(
            List<Trigger> orCondition,
            Product product)
        {
            _orCondition = orCondition;
            _product = product;
        }

        public void Check(Trigger notifiedTrigger)
        {
            foreach (var trriger in _orCondition)
                if (trriger.IsValid(notifiedTrigger))
                    if (OnTrrigered != null)
                        OnTrrigered();
        }

        public Product product
        {
            get { return _product; }
        }
    }
}