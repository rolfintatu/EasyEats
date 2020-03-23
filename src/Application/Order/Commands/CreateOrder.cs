using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Common;
using Application.Common.Exceptions;
using Application.Common.Dtos;

namespace Application.Order.Commands
{
    public class CreateOrder : IRequest
    {

        public CreateOrder(int reservationId)
            => (ReservationId) = (reservationId);

        public CreateOrder(int reservationId, string userId)
            : this(reservationId)
            => (UserId) = (userId);

        public string UserId { get; private set; }
        public int ReservationId { get; private set; }
    }

    public class CreateOrderHandler : IRequestHandler<CreateOrder>
    {
        private readonly IEasyEatsDbContext context;

        public CreateOrderHandler(IEasyEatsDbContext context)
            => (this.context) = (context);

        public async Task<Unit> Handle(CreateOrder request, CancellationToken cancellationToken)
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
