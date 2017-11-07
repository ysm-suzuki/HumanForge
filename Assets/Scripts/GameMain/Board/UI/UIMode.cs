using UnityMVC;

namespace GameMain
{
    public class UIMode : Model
    {
        public event EventHandler OnFinished;


        public delegate void UIModeEventHandler(UIMode uiMode);
        public event UIModeEventHandler OnModeChanged;


        protected Map _map = null;

        public void SetMap(Map map)
        {
            _map = map;
        }

        virtual public void End()
        {
            if (OnFinished != null)
                OnFinished();
        }

        virtual public void ClickMap()
        {
        }
        virtual public void ClickMap(Position position)
        {
        }

        virtual public void ClickUnit(Unit unit)
        {
        }

        protected void Change(UIMode next)
        {
            if (OnModeChanged != null)
                OnModeChanged(next);
        }
    }
}