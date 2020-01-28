using AppCore.Dtos;
using AppCore.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AppCore.Table.Queries.TableList
{
    public class TablesListQuery : IRequest<TablesListResponse>
    {
    }

    public class TablesListHandler : IRequestHandler<TablesListQuery, TablesListResponse>
    {
        private readonly IEasyEatsDbContext context;
        private readonly IMapper mapper;

        public TablesListHandler(IEasyEatsDbContext context
            ,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<TablesListResponse> Handle(TablesListQuery request, CancellationToken cancellationToken)
        {

            var tablesList = await context.Tables
                .ProjectTo<TableDetailsDto>(mapper.ConfigurationProvider)
                .ToListAsync();

            return new TablesListResponse() { Tables = tablesList };
 
        }
    }
}
