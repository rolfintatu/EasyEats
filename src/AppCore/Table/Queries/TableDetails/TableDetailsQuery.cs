using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AppCore.Dtos;
using AppCore.Exceptions;
using AppCore.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AppCore.Table.Queries.TableDetails
{
    public class TableDetailsQuery : IRequest<TableDetailsDto>
    {
        public int Id { get; set; }
    }

    public class TableDetailsHandler : IRequestHandler<TableDetailsQuery, TableDetailsDto>
    {
        private readonly IEasyEatsDbContext context;
        private readonly IMapper mapper;

        public TableDetailsHandler(IEasyEatsDbContext context
            ,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<TableDetailsDto> Handle(TableDetailsQuery request, CancellationToken cancellationToken)
        {

            var tableDetails = await context.Tables
                .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (tableDetails == null)
            {
                throw new NotFoundException(nameof(Table), request.Id);
            }

            tableDetails.Reservations = await context.Reservations.Where(x => x.TableId == request.Id).ToListAsync();

            return mapper.Map<TableDetailsDto>(tableDetails);

        }
    }
}
