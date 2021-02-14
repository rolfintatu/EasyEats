using Application.Common.Interfaces;
using Domain.Aggregates.ScheduleAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.AppSchedule.Commands.CreateSchedule
{
    public class CreateScheduleHandler : IRequestHandler<CreateScheduleCommand, Guid>
    {
        private readonly IScheduleRepository _repo;

        public CreateScheduleHandler(IScheduleRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        public async Task<Guid> Handle(CreateScheduleCommand request, CancellationToken cancellationToken)
        {
            if(!await _repo.ScheduleExist(request.Date))
            {
                var schedule = Schedule.CreateInstance(request.Date);
                var response = await _repo.CreateAsync(schedule);

                if (response == true)
                    return schedule.Id;
                else
                    return Guid.Empty;
            }
            else
            {
                return await _repo.GetScheduleIdByDate(request.Date);
            }
        }
    }
}
