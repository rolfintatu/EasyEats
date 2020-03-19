using Application.Common.Interfaces;
using Entities = Domain.Entities;
using System;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace Application.Bill.Queries
{
    public class BillByOrder : IRequest<BillByOrderResponse>
    {

        public BillByOrder(string orderId)
            => (this.OrderId) = (orderId);

        public string OrderId { get; set; }
    }

    public class BillByOrderResponse
    {

        public BillByOrderResponse(Entities.Bill order)
            => (this.Bill) = (order);

        public Entities.Bill Bill { get; set; }
    }

    public class BillByOrderHandler : IRequestHandler<BillByOrder, BillByOrderResponse>
    {

        private readonly IEasyEatsDbContext context;

        public BillByOrderHandler(IEasyEatsDbContext context)
            => (this.context) = (context);

        public async Task<BillByOrderResponse> Handle(BillByOrder request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new Exception();

            var bill = await context.Bills
                .AsNoTracking()
                .Include(x => x.Order)
                .FirstOrDefaultAsync(x => x.OrderId == request.OrderId);

            return new BillByOrderResponse(bill);
        }
    }
}