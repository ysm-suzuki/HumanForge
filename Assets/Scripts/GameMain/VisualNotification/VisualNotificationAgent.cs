using System.Collections.Generic;

using UnityMVC;

namespace GameMain
{
    public class VisualNotificationAgent
    {
        public delegate void EventHandler();
        public event EventHandler OnPhaseLocked;
        public event EventHandler OnPhaseUnlocked;

        private List<VisualNotification> _visualNotifications = new List<VisualNotification>();
        private Dictionary<VisualNotification.Product.Type, System.Action<VisualNotification.Product>> _productDelegates
            = new Dictionary<VisualNotification.Product.Type, System.Action<VisualNotification.Product>>();


        private bool _lockPhase = false;


        public  VisualNotificationAgent()
        {
            RegisterProductType(VisualNotification.Product.Type.ShowMessage, product => 
            {

            });
        }


        public void Add(VisualNotification visualNotification)
        {
            visualNotification.OnTrrigered += () =>
            {
                var type = visualNotification.product.type;

                UnityEngine.Debug.Assert(
                    _productDelegates.ContainsKey(type)
                    , "Triggered the unregistered gimmick");

                _productDelegates[type](visualNotification.product);
            };

            _visualNotifications.Add(visualNotification);
        }


        public void Notify(VisualNotification.Trigger trigger)
        {
            foreach (var visualNotification in _visualNotifications)
                visualNotification.Check(trigger);
        }



        public bool lockPhase
        {
            get { return _lockPhase; }
            private set
            {
                bool newCondition = value;

                if (_lockPhase == newCondition)
                    return;
                
                _lockPhase = value;

                if (_lockPhase)
                {
                    if (OnPhaseLocked != null)
                        OnPhaseLocked();
                }
                else
                {
                    if (OnPhaseUnlocked != null)
                        OnPhaseUnlocked();
                }
            }
        }



        private void RegisterProductType(VisualNotification.Product.Type type, System.Action<VisualNotification.Product> action)
        {
            _productDelegates[type] = action;
        }
    }
}