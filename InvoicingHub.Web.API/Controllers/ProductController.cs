using InvoicingHub.Application.UseCases.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InvoicingHub.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductUseCases _productUseCases;

        public ProductController(IProductUseCases productUseCases)
        {
            _productUseCases = productUseCases;
        }

        [HttpGet("products")]
        public async Task<IActionResult> GetProducts()
        {
            var result = await _productUseCases.GetProductsAsync();
            return StatusCode((int)result.StatusCode, result);
        }
    }
}