using ETL.Application.Products.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ETL.Controllers
{
    [Route("api/[controller]")]
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
            return StatusCode(StatusCodes.Status501NotImplemented, "This endpoint is not implemented yet.");
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            return StatusCode(StatusCodes.Status501NotImplemented, "This endpoint is not implemented yet.");
        }
    }
}
