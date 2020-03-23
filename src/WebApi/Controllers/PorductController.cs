using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Commands = Application.Product.Commands;
using Queries = Application.Product.Queries;
using Application.Common.Dtos;
using Domain.Enums;
using WebApi.Models;
using Dtos = Application.Common.Dtos;
using Microsoft.AspNetCore.Http;

namespace WebApi.Controllers
{
    [Route("EasyApi/[controller]")]
    [ApiController]
    public class PorductController : BaseController
    {
        //Queries
        [HttpGet]
        public async Task<IActionResult> ProductsList
            ([FromQuery] PriceFilter priceFilter, [FromQuery]Category category)
            => Ok(await mediator.Send(new Queries.ProductsList(priceFilter, category)));

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Details([FromRoute] int id)
            => Ok(await mediator.Send(new Queries.ProductDetails(id)));


        //Commands
        [HttpPut("Create")]
        public async Task Create([FromBody] ProductDto product)
            => await mediator.Send(new Commands.CreateProduct(product));

        [HttpPost("ChangePrice")]
        public async Task ChangePrice([FromBody] ChangeProductPriceModel productPrice)
            => await mediator.Send(new Commands.ChangeProductPrice(productPrice.NewPrice, productPrice.ProductId));

        [HttpPost("DecreaseQuantity")]
        public async Task DecreaseQuantity([FromBody] ProductQuantityModel product)
            => await mediator.Send(new Commands.DecreaseProductQuantity(product.ProductId, product.Quantity));

        [HttpPost("IncreaseQuantity")]
        public async Task IncreaseQuantity([FromBody] ProductQuantityModel product)
            => await mediator.Send(new Commands.IncreaseProductQuantity(product.ProductId, product.Quantity));

        [HttpDelete]
        public async Task Delete([FromBody] int productId)
            => await mediator.Send(new Commands.DeleteProduct(productId));
    }
}