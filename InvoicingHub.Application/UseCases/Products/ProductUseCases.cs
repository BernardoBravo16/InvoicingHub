using InvoicingHub.Application.DTO;
using InvoicingHub.Application.Shared.Contracts.Persistence;
using InvoicingHub.Application.Shared.Contracts.Responses;
using InvoicingHub.Application.UseCases.Interfaces;
using InvoicingHub.Domain.Entities;
using System.Net;

namespace InvoicingHub.Application.UseCases.Products
{
    public class ProductUseCases : IProductUseCases
    {
        private readonly IRepository<Product, int> _productRepository;

        public ProductUseCases(IRepository<Product, int> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ServiceResponse<List<ProductDTO>>> GetProductsAsync()
        {
            var response = new ServiceResponse<List<ProductDTO>>();

            try
            {
                var products = _productRepository.GetAll().ToList();

                response.Data = products.Select(c => new ProductDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    UnitPrice = c.UnitPrice,
                    Image = c.Image != null ? Convert.ToBase64String(c.Image) : null
                }).ToList();

                return response;
            }
            catch (Exception ex)
            {
                response.SetFaultyState(HttpStatusCode.InternalServerError, ex);
                return response;
            }
        }
    }
}
