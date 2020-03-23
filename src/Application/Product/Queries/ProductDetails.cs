using Application.Common.Dtos;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Product.Queries
{
    public class ProductDetails : IRequest<ProductDto>
    {
        public ProductDetails(int id)
            => (Id) = (id);

        public int Id { get; set; }
    }

    public class ProductHandler : IRequestHandler<ProductDetails, ProductDto>
    {

        private readonly IEasyEatsDbContext context;
        private readonly IMapper mapper;

        public ProductHandler(IEasyEatsDbContext context, IMapper mapper)
            => (this.context, this.mapper) = (context, mapper);

        public async Task<ProductDto> Handle(ProductDetails request, CancellationToken cancellationToken)
        {
            var baseResponse = await context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            var response = mapper.Map<ProductDto>(baseResponse);

            return response;
        }
    }
}
