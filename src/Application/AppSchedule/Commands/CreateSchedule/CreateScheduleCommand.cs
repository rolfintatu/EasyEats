using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.AppSchedule.Commands.CreateSchedule
{
    public class CreateScheduleCommand : IRequest<Guid>
    {

        public DateTime Date { get; set; }
    }
}
