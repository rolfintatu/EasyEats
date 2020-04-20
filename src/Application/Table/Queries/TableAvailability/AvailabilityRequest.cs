using System;
using System.Collections.Generic;
using System.Text;
using Domain.ValueObjects;
using MediatR;

namespace Application.Table.Queries.TableAvailability
{
    public class AvailabilityRequest : IRequest<AvailabilityResponse>
    {
        public AvailabilityRequest(int tableId, Date date, int? duration)
            => (this.TableId, this.Date, this.Duration) = (tableId, date, duration);

        public int TableId { get; set; }
        public Date Date { get; set; }
        public int? Duration { get; set; }
    }
}
