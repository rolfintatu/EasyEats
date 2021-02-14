using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using core = Domain.Aggregates.ScheduleAggregate;

namespace Application.Common.Interfaces
{
    public interface IScheduleRepository : IBaseRepository<core.Schedule>
    {
        Task<bool> ScheduleExist(DateTime date);
        Task<Guid> GetScheduleIdByDate(DateTime date);
    }
}
