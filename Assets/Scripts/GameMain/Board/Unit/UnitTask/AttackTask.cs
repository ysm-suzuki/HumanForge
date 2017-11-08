using System.Collections.Generic;

using UnityMVC;

namespace GameMain
{
    public class AttackTask : UnitTask
    {
        public event EventHandler OnAttackFired;

        private List<FieldObject> _targets = null;

        private float _warmUpSeconds;
        private float _coolDownSeconds;
        private float _elaspedSeconds;

        public bool preventTasks = false;

        public AttackTask(
            List<FieldObject> targets,
            float warmUpSeconds,
            float coolDownSeconds,
            Unit owner)
        {
            _targets = targets;
            _warmUpSeconds = warmUpSeconds;
            _coolDownSeconds = coolDownSeconds;
            _owner = owner;

            _elaspedSeconds = 0;
        }

        override public void Tick(float delta)
        {
            if (_isFinished)
                return;

            if (!AreTargetsLeft())
            {
                End();
                return;
            }

            base.Tick(delta);
            
            var nextElaspedSeconds = _elaspedSeconds + delta;

            if (nextElaspedSeconds >= _warmUpSeconds
                && _elaspedSeconds < _warmUpSeconds)
            {
                if (OnAttackFired != null)
                    OnAttackFired();
            }

            if (nextElaspedSeconds >= _warmUpSeconds + _coolDownSeconds
                && _elaspedSeconds < _warmUpSeconds + _coolDownSeconds)
            {
                End();
            }

            _elaspedSeconds = nextElaspedSeconds;
        }

        private bool AreTargetsLeft()
        {
            foreach (var target in _targets)
                if (target.isAlive)
                    return true;

            return false;
        }
    }
}