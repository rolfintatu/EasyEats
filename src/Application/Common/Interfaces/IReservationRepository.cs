using Domain.Aggregates.ScheduleAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IReservationRepository : IBaseRepository<Reservation>
    {
        Task<bool> CancelReservation(Guid reservationId);
    }
}
