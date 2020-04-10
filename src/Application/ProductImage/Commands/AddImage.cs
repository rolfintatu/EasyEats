using Application.Common.Dtos;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ProductImage.Commands
{
    public class AddImage : IRequest<List<Image>>
    {
        public AddImage(ImageStorageDto image, int productId)
        {
            this.Image = image;
            this.ProductId = productId;
        }

        public ImageStorageDto Image { get; set; }
        public int ProductId { get; set; }

    }

    public class AddImagesHandler : IRequestHandler<AddImage, List<Image>>
    {
        private readonly IEasyEatsDbContext context;
        private readonly IStorageService storage;

        public AddImagesHandler(IEasyEatsDbContext context, IStorageService storage)
        {
            this.context = context;
            this.storage = storage;
        }
        public async Task<List<Image>> Handle(AddImage request, CancellationToken cancellationToken)
        {
            var uri = await storage.UploadImage(request.Image.ByteArray, request.Image.Extension);
            var img = new Image(request.ProductId, uri.AbsoluteUri);
            await context.Images.AddAsync(img);
            await context.SaveChangesAsync(cancellationToken);

            return await context.Images.AsNoTracking().Where(x => x.ProductId.Equals(request.ProductId)).ToListAsync();
        }
    }
}
