using System.Collections.Generic;
using System.Linq;

namespace DistribuicaoLucros.Domain.Notification
{
    public class NotificationContext
    {
        private readonly List<Notification> notifications;
        public IReadOnlyCollection<Notification> Notifications => this.notifications;
        public bool HasNotifications => this.notifications.Any();


        public NotificationContext()
        {
            this.notifications = new List<Notification>();
        }


        public void AddNotification(string key, string message) => this.notifications.Add(new Notification(key, message));

        public void AddNotifications(IReadOnlyCollection<Notification> notificationsToAdd)
        {
            notifications.AddRange(notificationsToAdd);
        }
    }
}
