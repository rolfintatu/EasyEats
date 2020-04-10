using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Dtos;
using Application.ProductImage.Commands;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Common.Extensions;
using Application.ProductImage.Queries;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : BaseController
    {
        //Queries
        [HttpGet]
        public async Task<List<ImageDto>> GetByProductAsync(int productId)
            => await mediator.Send(new ImagesByProduct(productId));

        //Commands
        [HttpPost]
        public async Task<List<Image>> AddImageAsync(IFormFile image, [FromQuery] int productId)
            => await mediator.Send(new AddImage(new ImageStorageDto(image.ToByteArray(), image.GetExtention()), productId));

        [HttpDelete]
        public async Task DeleteImage(int imageId)
            => await mediator.Send(new DeleteImage(imageId));

    }
}