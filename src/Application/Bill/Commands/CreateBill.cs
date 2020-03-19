using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Entities = Domain.Entities;

namespace Application.Bill.Commands
{
    public class CreateBill : IRequest<CreateBillResponse>
    {

        public CreateBill(Entities.Order order) =>
            (this.Order) = (order);

        public Entities.Order Order { get; set; }
    }

    public class CreateBillResponse
    {
        public CreateBillResponse(Entities.Bill bill) =>
            (this.Bill) = (bill);

        public Entities.Bill Bill { get; }
    }

    public class CreateBillHandler : IRequestHandler<CreateBill, CreateBillResponse>
    {

        private readonly IEasyEatsDbContext context;

        public CreateBillHandler(IEasyEatsDbContext context) =>
            (this.context) = (context);

        public async Task<CreateBillResponse> Handle(CreateBill request, CancellationToken cancellationToken)
        {
            _ = request.Order ?? throw new Exception();

            foreach (Entities.OrderItems item in request.Order.OrderItems)
            {
                var product = await context.Products.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == item.ProductId);

                product.DecreaseQuantity(item.Quantity);
                item.GetTotal();
            }

            var bill = new Entities.Bill(request.Order.Id, request.Order);

            bill.CalculateSubTotal();
            var TotalBill = bill.ApplyTax();

            await context.Bills.AddAsync(TotalBill);

            await context.SaveChangesAsync(cancellationToken);

            return new CreateBillResponse(TotalBill);
        }
    }
}
