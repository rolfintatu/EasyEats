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

namespace AppCore.Food.Queries.GetFoods
{
    public class FoodListHandler : IRequestHandler<FoodListRequest, FoodList>
    {
        private readonly IEasyEatsDbContext context;
        private readonly IMapper mapper;

        public FoodListHandler(IEasyEatsDbContext context
            ,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<FoodList> Handle(FoodListRequest request, CancellationToken cancellationToken)
        {
            var foodList = await context.Foods
                .ProjectTo<FoodModel>(mapper.ConfigurationProvider)
                .ToListAsync();

            var result = new FoodList()
            {
                Foods = foodList
            };

            return result;
        }
    }
}
