using Application.Common.Interfaces;
using Domain.Aggregates.ScheduleAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.AppReservation.Commands.CreateReservation
{
    public class CreateReservationHandler : IRequestHandler<CreateReservationCommand, bool>
    {
        private readonly IReservationRepository _repo;

        public CreateReservationHandler(IReservationRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        public async Task<bool> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
                return false;

            var reservation = Reservation.CreateInstance(
                    request.Date, request.StartHour, request.StartMinutes, 
                    request.Duration, request.CustonerName,
                    request.ScheduleId
                );

            var response = await _repo.CreateAsync(reservation);

            return response;
        }
    }
}
