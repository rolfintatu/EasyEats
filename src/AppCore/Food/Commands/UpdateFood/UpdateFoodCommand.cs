using AppCore.Enums;
using AppCore.Exceptions;
using AppCore.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AppCore.Food.Commands.UpdateFood
{
    public class UpdateFoodCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Category Category { get; set; }
        public int Calories { get; set; }
        public string Description { get; set; }
    }

    public class UpdateFoodHandler : IRequestHandler<UpdateFoodCommand>
    {
        private readonly IEasyEatsDbContext context;

        public UpdateFoodHandler(IEasyEatsDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(UpdateFoodCommand request, CancellationToken cancellationToken)
        {
            var food = await context.Foods.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);


            if (food == null)
            {
                throw new NotFoundException(nameof(Food), request.Id);
            }

            food.Name = request.Name;
            food.Price = request.Price;
            food.Quantity = request.Quantity;
            food.Calories = request.Calories;
            food.Category = request.Category;
            food.Description = request.Description;

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;

        }
    }
}
