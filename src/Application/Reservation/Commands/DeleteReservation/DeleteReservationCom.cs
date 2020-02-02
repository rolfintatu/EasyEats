using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Reservation.Commands.DeleteReservation
{
    public class DeleteReservationCom : IRequest
    {
        public int Id { get; set; }

    }

    public class DeleteReservationHandler : IRequestHandler<DeleteReservationCom>
    {
        private readonly IEasyEatsDbContext context;
        private readonly ICurrentUserService userService;

        public DeleteReservationHandler(IEasyEatsDbContext context
            ,ICurrentUserService userService)
        {
            this.context = context;
            this.userService = userService;
        }
        public async Task<Unit> Handle(DeleteReservationCom request, CancellationToken cancellationToken)
        {

            var reservation = await context.Reservations
                .SingleOrDefaultAsync(x => x.CustomerId == userService.UserId && x.Id == request.Id);

            if (reservation == null)
            {
                throw new NotFoundException(nameof(DeleteReservationCom), request.Id);
            }

            context.Reservations.Remove(reservation);

            return Unit.Value;

        }
    }
}
