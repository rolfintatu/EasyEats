using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Entities = Domain.Entities;

namespace Application.Customer.Commands.CreateCustomer
{
    public class CreateCustomerCommand : IRequest
    {
        public CreateCustomerCommand()
        {
        }

        public CreateCustomerCommand(
            string id
            , string name
            , int numberPhone)
        {
            Id = id;
            Name = name;
            NumberPhone = numberPhone;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public int NumberPhone { get; set; }
    }

    public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand>
    {
        private readonly IEasyEatsDbContext context;
        private readonly IMediator mediator;

        public CreateCustomerHandler(IEasyEatsDbContext context
            ,IMediator mediator)
        {
            this.context = context;
            this.mediator = mediator;
        }

        // TODO: Change number phone type
        public async Task<Unit> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            await context.Customers.AddAsync(new Entities.Customer { Id = request.Id, Name = request.Name, Phone = request.NumberPhone });

            var DbResponse = await context.SaveChangesAsync(cancellationToken);

            await mediator.Publish(new CustomerCreated() { Id = request.Id, Name = request.Name, DbResponse = DbResponse });
            
            return Unit.Value;
        }
    }
}
