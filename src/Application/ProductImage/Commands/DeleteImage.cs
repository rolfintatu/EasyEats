using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ProductImage.Commands
{
    public class DeleteImage : IRequest
    {
        public DeleteImage(int imageId)
        {
            ImageId = imageId;
        }

        public int ImageId { get; set; }
    }

    public class DeleteImageHandler : IRequestHandler<DeleteImage>
    {
        private readonly IEasyEatsDbContext context;
        private readonly IStorageService storage;

        public DeleteImageHandler(IEasyEatsDbContext context, IStorageService storage)
        {
            this.context = context;
            this.storage = storage;
        }

        public async Task<Unit> Handle(DeleteImage request, CancellationToken cancellationToken)
        {

            var image = await context.Images.AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(request.ImageId));

            _ = image ?? throw new Exception();

            var imageName = image.Url.Split("/").ToList().LastOrDefault();

            await storage.DeleteImage(imageName);

            context.Images.Remove(image);

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
