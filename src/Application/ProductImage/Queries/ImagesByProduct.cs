using Application.Common.Interfaces;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Dtos;
using AutoMapper;

namespace Application.ProductImage.Queries
{
    public class ImagesByProduct: IRequest<List<ImageDto>>
    {
        public ImagesByProduct(int productId)
        {
            ProductId = productId;
        }

        public int ProductId { get; set; }
    }

    public class ImageByProductHandle : IRequestHandler<ImagesByProduct, List<ImageDto>>
    {
        private readonly IEasyEatsDbContext context;
        private readonly IMapper mapper;

        public ImageByProductHandle(IEasyEatsDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<List<ImageDto>> Handle(ImagesByProduct request, CancellationToken cancellationToken)
        {
            return await context.Images
                .AsNoTracking()
                .Where(x => x.ProductId.Equals(request.ProductId))
                .ProjectTo<ImageDto>(mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}
