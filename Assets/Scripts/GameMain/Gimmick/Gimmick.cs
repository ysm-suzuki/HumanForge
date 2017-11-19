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

        private List<Trigger> _orCondition;


        public Gimmick(
            List<Trigger> orCondition)
        {
            _orCondition = orCondition;
        }

        public void Check(Trigger notifiedTrriger)
        {
            foreach (var trriger in _orCondition)
                if (trriger.IsValid(notifiedTrriger))
                    if (OnTrrigered != null)
                        OnTrrigered();
        }
    }
}