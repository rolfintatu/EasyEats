using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Domain.Exceptions;

namespace Application.Table.Commands.UpdateTableStatus
{
    public class UpdateTableStatusCom : IRequest
    {
        public int Id { get; set; }
        public TableStatus Status { get; set; }
    }

    public class UpdateTableStatusHandler : IRequestHandler<UpdateTableStatusCom>
    {
        private readonly IEasyEatsDbContext context;

        public UpdateTableStatusHandler(IEasyEatsDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(UpdateTableStatusCom request, CancellationToken cancellationToken)
        {

            var table = await context.Tables
                .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (table == null)
            {
                throw new NotFoundException(nameof(Table), request.Id);
            }

            table.ChangeStatus(request.Status);

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
