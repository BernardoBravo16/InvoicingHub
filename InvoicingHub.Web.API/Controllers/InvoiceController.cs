using InvoicingHub.Application.DTO;
using InvoicingHub.Application.UseCases.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InvoicingHub.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoicesUseCases _invoicesUseCases;

        public InvoiceController(IInvoicesUseCases invoicesUseCases)
        {
            _invoicesUseCases = invoicesUseCases;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateInvoice([FromBody] CreateInvoiceCommand command)
        {
            var result = await _invoicesUseCases.Create(command);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet("by-number/{invoiceNumber}")]
        public async Task<IActionResult> GetByInvoiceNumber(string invoiceNumber)
        {
            var result = await _invoicesUseCases.GetInvoice(invoiceNumber);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet("by-customer/{customerId}")]
        public async Task<IActionResult> GetByCustomerId(int customerId)
        {
            var result = await _invoicesUseCases.GetInvoiceByCustomer(customerId);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}