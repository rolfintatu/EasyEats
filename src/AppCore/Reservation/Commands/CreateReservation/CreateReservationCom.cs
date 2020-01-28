using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AppCore.Exceptions;
using AppCore.Interfaces;
using AppCore.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AppCore.Reservation.Commands.CreateReservation
{
    public class CreateReservationCom : IRequest
    {
        public Date Date { get; set; }
        public int Hour { get; set; }
        public int Duration { get; set; }

        //public string CustomerId { get; set; }
        //public Entities.Customer Customer { get; set; }

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

            var customer = await context.Customers
                .SingleOrDefaultAsync(x => x.Id == userService.UserId, cancellationToken);

            var table = await context.Tables
                .SingleOrDefaultAsync(x => x.Id == request.TableId, cancellationToken);

            if (customer == null || table == null)
            {
                throw new NotFoundException(nameof(Entities.Reservation), request.TableId);
            }

            await context.Reservations.AddAsync(new Entities.Reservation()
            {
                Date = request.Date,
                Hour = request.Hour,
                Duration = request.Duration,
                CustomerId = customer.Id,
                Customer = customer,
                TableId = request.TableId,
                Table = table
            });

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;

        }
    }
}
