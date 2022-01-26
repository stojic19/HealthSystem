using Integration.Database.EfStructures;
using Integration.Shared.Model;
using Integration.Shared.Repository.Base;

namespace Integration.Shared.Repository.Implementation
{
    public class NotificationReadRepository : ReadBaseRepository<int, Notification>, INotificationReadRepository
    {
        public NotificationReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
