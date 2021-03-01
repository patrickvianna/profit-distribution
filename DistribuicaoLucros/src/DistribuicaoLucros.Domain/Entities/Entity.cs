using Flunt.Notifications;
using N = DistribuicaoLucros.Domain.Notification.Notification;

namespace DistribuicaoLucros.Domain.Entities
{
    public abstract class Entity : Notifiable<N>
    {
        public bool Invalid => !this.IsValid;
    }
}
