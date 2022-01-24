using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Integration.Shared.Model;
using Integration.Shared.Repository.Base;

namespace Integration.Shared.Repository
{
    public interface INotificationReadRepository : IReadBaseRepository<int, Notification>
    {
    }
}
