using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Entities = Domain.Entities;

namespace Application.Reservation.Commands.CreateReservation
{
    public class CreateReservationCom : IRequest
    {
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int Hour { get; set; }
        public int Duration { get; set; }
        public int TableId { get; set; }

    }

    public class CreateReservationHandler : IRequestHandler<CreateReservationCom>
    {
        private readonly IEasyEatsDbContext context;
        private readonly ICurrentUserService userService;

        public CreateReservationHandler(IEasyEatsDbContext context
            ,ICurrentUserService userService)
        {
            this.context = context;
            this.userService = userService;
        }
 
        public async Task<Unit> Handle(CreateReservationCom request, CancellationToken cancellationToken)
        {

            if (userService.UserId == null)
            {
                throw new ArgumentNullException(userService.UserId);
            }

            await context.Reservations.AddAsync(
                new Entities.Reservation(
                        new Date(request.Day, request.Month, request.Year),
                        request.Hour,
                        userService.UserId,
                        request.TableId,
                        request.Duration
                    ));

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;

        }
    }
}
