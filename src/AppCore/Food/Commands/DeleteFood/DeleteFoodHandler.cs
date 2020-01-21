using AppCore.Exceptions;
using AppCore.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AppCore.Food.Commands.DeleteFood
{
    public class DeleteFoodHandler : IRequestHandler<DeleteFoodCommand>
    {
        private readonly IEasyEatsDbContext context;

        public DeleteFoodHandler(IEasyEatsDbContext _context)
        {
            context = _context;
        }

        public async Task<Unit> Handle(DeleteFoodCommand request, CancellationToken cancellationToken)
        {
            var exitsFood = await context.Foods.FindAsync(request.Id);

            if (exitsFood == null)
            {
                throw new NotFoundException(nameof(Food), request.Id);
            }

            context.Foods.Remove(exitsFood);

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
