using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AppCore.Enums;
using AppCore.Interfaces;
using MediatR;

namespace AppCore.Table.Commands.CreateTable
{
    public class CreateTableCommand : IRequest
    {
        public int ChairsCount { get; set; }
        public TableStatus Status { get; set; }
    }

    public class CreateTableHandler : IRequestHandler<CreateTableCommand>
    {
        private readonly IEasyEatsDbContext context;
        private readonly IMediator mediator;

        public CreateTableHandler(IEasyEatsDbContext context
            ,IMediator mediator)
        {
            this.context = context;
            this.mediator = mediator;
        }

        public async Task<Unit> Handle(CreateTableCommand request, CancellationToken cancellationToken)
        {
            await context.Tables.AddAsync(new Entities.Table()
            {
                ChairsCount = request.ChairsCount,
                Status = request.Status
            });

            await context.SaveChangesAsync(cancellationToken);

            await mediator.Publish(new TableCreated());

            return Unit.Value;

        }
    }
}
