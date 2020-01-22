using AppCore.Enums;
using AppCore.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AppCore.Drink.Commands.CreateDrink
{
    public class CreateDrinkCommand : IRequest
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int Calories { get; set; }
        public Category Category { get; set; }
        public string Description { get; set; }
    }

    public class CreateDrinkHandler : IRequestHandler<CreateDrinkCommand>
    {
        private readonly IEasyEatsDbContext context;
        private readonly IMediator mediator;

        public CreateDrinkHandler(IEasyEatsDbContext context
            ,IMediator mediator)
        {
            this.context = context;
            this.mediator = mediator;
        }

        public async Task<Unit> Handle(CreateDrinkCommand request, CancellationToken cancellationToken)
        {
            var entity = new Entities.Drink()
            {
                Name = request.Name,
                Price = request.Price,
                Description = request.Description,
                Calories = request.Calories,
                Category = request.Category
            };

            context.Drinks.Add(entity);

            await mediator.Publish(new DrinkCreated() { DrinkName = entity.Name });

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;

        }
    }
}
