using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.AppReservation.Commands.CreateReservation
{
    public class CreateReservationCommand : IRequest<bool>
    {
        public Guid ScheduleId { get; set; }
        public DateTime Date { get; set; } 
        public int StartHour { get; set; }
        public int Duration { get; set; }
        public string CustonerName { get; set; }
    }
}
