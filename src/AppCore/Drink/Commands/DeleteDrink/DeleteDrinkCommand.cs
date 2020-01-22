using AppCore.Exceptions;
using AppCore.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AppCore.Drink.Commands.DeleteDrink
{
    public class DeleteDrinkCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteDrinkHandler : IRequestHandler<DeleteDrinkCommand>
    {
        private readonly IEasyEatsDbContext context;

        public DeleteDrinkHandler(IEasyEatsDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(DeleteDrinkCommand request, CancellationToken cancellationToken)
        {
            var deleteObj = await context.Drinks.FindAsync(request.Id);

            if (deleteObj == null)
            {
                throw new NotFoundException(nameof(Drink), request.Id);
            }

            context.Drinks.Remove(deleteObj);

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;

        }
    }
}
