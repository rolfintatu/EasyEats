using AppCore.Enums;
using AppCore.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using AppCore.Exceptions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AppCore.Drink.Commands.UpdateDrink
{
    public class UpdateDrinkCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Category Category { get; set; }
        public int Calories { get; set; }
        public string Description { get; set; }
    }

    public class UpdateDrinkHandler : IRequestHandler<UpdateDrinkCommand>
    {
        private readonly IEasyEatsDbContext context;

        public UpdateDrinkHandler(IEasyEatsDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(UpdateDrinkCommand request, CancellationToken cancellationToken)
        {

            var dbDrink = await context.Drinks
                .SingleOrDefaultAsync(x => x.Id == request.Id);

            if (dbDrink == null)
            {
                throw new NotFoundException(nameof(Entities.Drink), request.Id);
            }

            dbDrink.Name = request.Name;
            dbDrink.Price = request.Price;
            dbDrink.Quantity = request.Quantity;
            dbDrink.Calories = request.Calories;
            dbDrink.Category = request.Category;
            dbDrink.Description = request.Description;

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;

        }
    }
}
