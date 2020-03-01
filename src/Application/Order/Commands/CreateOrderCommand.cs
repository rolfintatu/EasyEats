using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Common;
using Domain.Exceptions;

namespace Application.Order.Commands
{
    public class CreateOrderCommand : IRequest
    {

        public CreateOrderCommand(
            int reservationId
            )
        {
            this.ReservationId = reservationId;
        }

        public CreateOrderCommand(
            int reservationId
            , string userId
            )
            :this(reservationId)
        {
            this.ReservationId = reservationId;
            this.UserId = userId;
        }

        public string UserId { get; private set; }
        public int ReservationId { get; private set; }
    }

    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand>
    {
        private readonly IEasyEatsDbContext context;

        public CreateOrderHandler(
            IEasyEatsDbContext context
            )
        {
            this.context = context;
        }

        public async Task<Unit> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {


            if (!string.IsNullOrEmpty(request.UserId))
            {
                var orderId = Generator.GenerateId();

                await context.Orders.AddAsync(new Domain.Entities.Order(request.UserId, orderId));
            }
            else
            {
                var reservation = await context.Reservations
                    .AsNoTracking()
                    .SingleOrDefaultAsync(x => x.Id == request.ReservationId);

                if (reservation == null)
                {
                    throw new NotFoundException(nameof(Reservation), request.ReservationId);
                }
                else
                {
                    var orderId = Generator.GenerateId();

                    await context.Orders.AddAsync(new Domain.Entities.Order(reservation.CustomerId, orderId));

                    reservation.SetOrderId(orderId);
                }
            }

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
