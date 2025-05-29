using InvoicingHub.Application.UseCases.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InvoicingHub.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerUseCases _customerUseCases;

        public CustomerController(ICustomerUseCases customerUseCases)
        {
            _customerUseCases = customerUseCases;
        }

        [HttpGet("customers")]
        public async Task<IActionResult> GetCustomers()
        {
            var result = await _customerUseCases.GetCustomersAsync();
            return StatusCode((int)result.StatusCode, result);
        }
    }
}