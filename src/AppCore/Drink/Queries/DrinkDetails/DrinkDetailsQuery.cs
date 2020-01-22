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

namespace AppCore.Drink.Queries.DrinkDetails
{
    public class DrinkDetailsQuery : IRequest<DrinkDetails>
    {
        public int Id { get; set; }
    }

    public class DrinkDetailsHandler : IRequestHandler<DrinkDetailsQuery, DrinkDetails>
    {
        private readonly IEasyEatsDbContext context;
        private readonly IMapper mapper;

        public DrinkDetailsHandler(IEasyEatsDbContext context
            , IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<DrinkDetails> Handle(DrinkDetailsQuery request, CancellationToken cancellationToken)
        {
            var drink = await context.Drinks
                .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (drink == null)
            {
                throw new NotFoundException(nameof(Entities.Drink), request.Id);
            }

            return mapper.Map<DrinkDetails>(drink);

        }
    }
}
