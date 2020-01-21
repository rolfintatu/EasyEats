using AppCore.Exceptions;
using AppCore.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AppCore.Food.Queries.FoodDetails
{
    public class FoodDetailsHandler : IRequestHandler<FoodDetailsRequest, FoodDetails>
    {
        private readonly IEasyEatsDbContext context;
        private readonly IMapper mapper;

        public FoodDetailsHandler(IEasyEatsDbContext context
            ,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<FoodDetails> Handle(FoodDetailsRequest request, CancellationToken cancellationToken)
        {
            var result = await context.Foods.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (result == null)
            {
                throw new NotFoundException(nameof(Food), request.Id);
            }

            return mapper.Map<FoodDetails>(result);
        }
    }
}
