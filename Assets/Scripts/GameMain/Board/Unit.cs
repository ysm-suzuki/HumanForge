using System.Collections.Generic;

using UnityMVC;

namespace GameMain
{
    public class Unit : FieldObject
    {
        private static int s_serialCounter = 0;
        private int _serialId = -1;


        private UnitData _data;

        private MoveTo _moveTo = null;

        public Unit(UnitData data)
        {
            _serialId = ++s_serialCounter;

            _data = data;

            life = maxLife = _data.life;

            shapePoints = new List<Position>
            {
                Position.Create(-10, 10),
                Position.Create(10, 10),
                Position.Create(10, -10),
                Position.Create(-10, -10)
            };
        }



        override public void Tick(float delta)
        {
            base.Tick(delta);

            if (_moveTo != null)
                _moveTo.Tick(delta);
        }



        public void MoveTo(Position destination)
        {
            _moveTo = new MoveTo(destination, this);
            _moveTo.OnFinished += () => 
            {
                _moveTo = _moveTo.next;
            };
        }



        public bool IsSame(Unit unit)
        {
            return unit.serialId == serialId;
        }

        public int serialId
        {
            get { return _serialId; }
        }

        public float attack
        {
            get { return _data.attack; }
        }
        public float defence
        {
            get { return _data.defence; }
        }
        public float attackSpeed
        {
            get { return _data.attackSpeed; }
        }
        public float moveSpeed
        {
            get { return _data.moveSpeed; }
        }
    }
}