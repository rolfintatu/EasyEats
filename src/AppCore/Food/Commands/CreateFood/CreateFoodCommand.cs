using AppCore.Enums;
using AppCore.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AppCore.Food.Commands.CreateFood
{
    public class CreateFoodCommand : IRequest
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Category Category { get; set; }
        public int Calories { get; set; }
        public string Description { get; set; }
    }

    public class Handler : IRequestHandler<CreateFoodCommand>
    {

        private readonly IEasyEatsDbContext _dbContext;
        private readonly IMediator _mediator;

        public Handler(IEasyEatsDbContext dbContext,
            IMediator mediator)
        {
            this._dbContext = dbContext;
            this._mediator = mediator;
        }

        public async Task<Unit> Handle(CreateFoodCommand request, CancellationToken cancellationToken)
        {

            var food = new Entities.Food()
            {

                Name = request.Name,
                Price = request.Price,
                Quantity = request.Quantity,
                Category = request.Category,
                Calories = request.Calories,
                Description = request.Description,

            };

            _dbContext.Foods.Add(food);

            await _dbContext.SaveChangesAsync(cancellationToken);

            await _mediator.Publish(new FoodCreated() { FoodId = request.Id }, cancellationToken);

            return Unit.Value;
        }
    }
}
