using Integration.Database.EfStructures;
using Integration.Shared.Model;
using Integration.Shared.Repository.Base;

namespace Integration.Shared.Repository.Implementation
{
    public class NotificationWriteRepository : WriteBaseRepository<Notification>, INotificationWriteRepository
    {
        public NotificationWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
