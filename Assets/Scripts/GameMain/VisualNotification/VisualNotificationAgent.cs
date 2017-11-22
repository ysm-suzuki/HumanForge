using System.Collections.Generic;

using UnityMVC;

namespace GameMain
{
    public class VisualNotificationAgent
    {
        private List<VisualNotification> _visualNotifications = new List<VisualNotification>();

        public void Add(VisualNotification visualNotification)
        {
            _visualNotifications.Add(visualNotification);
        }
    }
}