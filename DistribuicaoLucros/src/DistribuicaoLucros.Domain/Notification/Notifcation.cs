namespace DistribuicaoLucros.Domain.Notification
{
    public class Notification : Flunt.Notifications.Notification
    {
        public Notification(string key, string message)
        {
            this.Key = key;
            this.Message = message;
        }
    }
}
