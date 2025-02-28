using ETL.Application.Products.Commands;
using ETL.Application.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ETL.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(CreateProduct), new { id }, id);
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _mediator.Send(new GetProductsQuery());
            return Ok(products);
        }
    }
}
