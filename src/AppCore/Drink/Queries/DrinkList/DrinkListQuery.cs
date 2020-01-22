using AppCore.Common;
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

namespace AppCore.Drink.Queries.DrinkList
{
    public class DrinkListQuery : IRequest<DrinkListResponse>
    {
    }

    public class DrinkListHandler :
        IRequestHandler<DrinkListQuery, DrinkListResponse>
    {
        private readonly IEasyEatsDbContext context;
        private readonly IMapper mapper;

        public DrinkListHandler(IEasyEatsDbContext context
            , IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<DrinkListResponse> Handle(DrinkListQuery request, CancellationToken cancellationToken)
        {
            var drinkList = await context.Drinks
                .ProjectTo<DrinkModel>(mapper.ConfigurationProvider)
                .ToListAsync();

            var result = new DrinkListResponse()
            {
                Drinks = drinkList,
            };

            return result;

        }
    }
}
