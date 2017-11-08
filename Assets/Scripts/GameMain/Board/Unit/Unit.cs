﻿using System.Collections.Generic;

using UnityMVC;

namespace GameMain
{
    public class Unit : FieldObject
    {
        private static int s_serialCounter = 0;
        private int _serialId = -1;


        private UnitData _data;

        private Recognition _recognition = null;

        // attributes
        public bool isPlayerUnit = false;
        public bool isOwnedUnit = false;
        //

        private UnitTaskAgent _taskAgent = null;
        

        public Unit(UnitData data)
        {
            _serialId = ++s_serialCounter;

            _data = data;

            life = maxLife = _data.life;

            const float PI = UnityEngine.Mathf.PI;
            shapePoints = new List<Position>
            {
                Position.Create(
                    _data.sizeRadius * UnityEngine.Mathf.Cos((22.5f + 45 * 0) * PI / 180),
                    _data.sizeRadius * UnityEngine.Mathf.Sin((22.5f + 45 * 0) * PI / 180)),
                Position.Create(
                    _data.sizeRadius * UnityEngine.Mathf.Cos((22.5f + 45 * -1) * PI / 180),
                    _data.sizeRadius * UnityEngine.Mathf.Sin((22.5f + 45 * -1) * PI / 180)),
                Position.Create(
                    _data.sizeRadius * UnityEngine.Mathf.Cos((22.5f + 45 * -2) * PI / 180),
                    _data.sizeRadius * UnityEngine.Mathf.Sin((22.5f + 45 * -2) * PI / 180)),
                Position.Create(
                    _data.sizeRadius * UnityEngine.Mathf.Cos((22.5f + 45 * -3) * PI / 180),
                    _data.sizeRadius * UnityEngine.Mathf.Sin((22.5f + 45 * -3) * PI / 180)),
                Position.Create(
                    _data.sizeRadius * UnityEngine.Mathf.Cos((22.5f + 45 * -4) * PI / 180),
                    _data.sizeRadius * UnityEngine.Mathf.Sin((22.5f + 45 * -4) * PI / 180)),
                Position.Create(
                    _data.sizeRadius * UnityEngine.Mathf.Cos((22.5f + 45 * -5) * PI / 180),
                    _data.sizeRadius * UnityEngine.Mathf.Sin((22.5f + 45 * -5) * PI / 180)),
                Position.Create(
                    _data.sizeRadius * UnityEngine.Mathf.Cos((22.5f + 45 * -6) * PI / 180),
                    _data.sizeRadius * UnityEngine.Mathf.Sin((22.5f + 45 * -6) * PI / 180)),
                Position.Create(
                    _data.sizeRadius * UnityEngine.Mathf.Cos((22.5f + 45 * -7) * PI / 180),
                    _data.sizeRadius * UnityEngine.Mathf.Sin((22.5f + 45 * -7) * PI / 180)),
            };

            _recognition = new Recognition(this);
            _taskAgent = new UnitTaskAgent(this);
        }



        override public void Tick(float delta)
        {
            base.Tick(delta);

            _taskAgent.Tick(delta);
        }


        // ===================================== actions

        public void MoveTo(Position destination)
        {
            _taskAgent.MoveTo(destination);
        }

        public void Attack(List<FieldObject> targets)
        {
            _taskAgent.Attack(targets);
        }


        // =================================== conditions

        public bool IsInSight(FieldObject target)
        {
            float sightRange = 300;

            var distance = (target.position - position).ToVector().GetLength();

            return distance <= sightRange;
        }


        public bool IsSame(Unit unit)
        {
            return unit.serialId == serialId;
        }


        // ======================================= accessors

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

        public float sizeRadius
        {
            get { return _data.sizeRadius; }
        }


        public Recognition recognition
        {
            get { return _recognition; }
        }
    }
}