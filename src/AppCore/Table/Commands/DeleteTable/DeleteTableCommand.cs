using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AppCore.Exceptions;
using AppCore.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AppCore.Table.Commands.DeleteTable
{
    public class DeleteTableCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteTableHandler : IRequestHandler<DeleteTableCommand>
    {
        private readonly IEasyEatsDbContext context;

        public DeleteTableHandler(IEasyEatsDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(DeleteTableCommand request, CancellationToken cancellationToken)
        {

            var table = await context.Tables
                .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (table == null)
            {
                throw new NotFoundException(nameof(Table), request.Id);
            }

            context.Tables.Remove(table);

            return Unit.Value;
        }
    }
}
