using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.AppReservation.Commands.CancelReservation
{
    public class CancelReservationHandler : IRequestHandler<CancelReservationCommand, bool>
    {
        private readonly IReservationRepository _repo;

        public CancelReservationHandler(IReservationRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        public async Task<bool> Handle(CancelReservationCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
                return false;

            var response = await _repo.CancelReservation(request.ReservationId);

            return response;
        }
    }
}
