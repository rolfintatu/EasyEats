using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Domain.Enums;

namespace Application.Reservation.Commands.DeleteReservation
{
    public class CancelReservationCom : IRequest
    {
        public int Id { get; set; }

    }

    public class CancelReservationHandler : IRequestHandler<CancelReservationCom>
    {
        private readonly IEasyEatsDbContext context;
        private readonly ICurrentUserService userService;

        public CancelReservationHandler(IEasyEatsDbContext context
            ,ICurrentUserService userService)
        {
            this.context = context;
            this.userService = userService;
        }
        public async Task<Unit> Handle(CancelReservationCom request, CancellationToken cancellationToken)
        {

            var reservation = await context.Reservations
                .SingleOrDefaultAsync(x => x.CustomerId == userService.UserId && x.Id == request.Id);

            if (reservation == null)
            {
                throw new NotFoundException(nameof(CancelReservationCom), request.Id);
            }

            //context.Reservations.Remove(reservation);
            reservation.CancelReservation();

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;

        }
    }
}
