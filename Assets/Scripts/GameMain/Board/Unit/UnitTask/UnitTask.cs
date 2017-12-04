using System.Collections.Generic;

using UnityMVC;

namespace GameMain
{
    public class UnitTask
    {
        public delegate void EventHandler();
        public event EventHandler OnFinished;

        protected Unit _owner = null;
        protected bool _isFinished = false;

        virtual public void Tick(float delta)
        {
        }

        public void Cancel()
        {
            End();
        }

        virtual protected void End()
        {
            _isFinished = true;
            if (OnFinished != null)
                OnFinished();
        }
    }
}