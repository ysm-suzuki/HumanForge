using System.Collections.Generic;

using UnityMVC;

namespace GameMain
{
    public class AttackTask
    {
        public delegate void EventHandler();
        public event EventHandler OnAttackFired;
        public event EventHandler OnFinished;

        private Unit _owner = null;
        private List<FieldObject> _targets = null;

        private float _warmUpSeconds;
        private float _coolDownSeconds;
        private float _elaspedSeconds;
        private bool _isFinished = false;

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

        public void Tick(float delta)
        {
            if (_isFinished)
                return;

            if (!AreTargetsLeft())
            {
                if (OnFinished != null)
                    OnFinished();

                _isFinished = true;

                return;
            }

            
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
                if (OnFinished != null)
                    OnFinished();

                _isFinished = true;
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