using System.Collections.Generic;

using UnityMVC;

namespace GameMain
{
    public class VisualNotification
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
                ShowMessage
            }
            public Type type = Type.None;
            public int valueI = 0;
            public string valueS = "";
        }

        private List<Trigger> _orCondition;
        private Product _product;


        public VisualNotification(
            List<Trigger> orCondition,
            Product product)
        {
            _orCondition = orCondition;
            _product = product;
        }


        public void Check(Trigger notifiedTrriger)
        {
            foreach (var trriger in _orCondition)
                if (trriger.IsValid(notifiedTrriger))
                    if (OnTrrigered != null)
                        OnTrrigered();
        }

        public Product product
        {
            get { return _product; }
        }
    }
}