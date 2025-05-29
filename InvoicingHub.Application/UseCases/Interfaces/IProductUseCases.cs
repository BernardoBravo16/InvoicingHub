using InvoicingHub.Application.DTO;
using InvoicingHub.Application.Shared.Contracts.Responses;

namespace InvoicingHub.Application.UseCases.Interfaces
{
    public interface IProductUseCases
    {
        Task<ServiceResponse<List<ProductDTO>>> GetProductsAsync();
    }
}