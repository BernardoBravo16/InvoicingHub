using InvoicingHub.Application.DTO;
using InvoicingHub.Application.Shared.Contracts.Persistence;
using InvoicingHub.Application.Shared.Contracts.Responses;
using InvoicingHub.Application.UseCases.Interfaces;
using InvoicingHub.Domain.Entities;
using System.Net;

namespace InvoicingHub.Application.UseCases.Customers
{
    public class CustomerUseCases : ICustomerUseCases
    {
        private readonly IRepository<Customer, int> _customerRepository;

        public CustomerUseCases(IRepository<Customer, int> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<ServiceResponse<List<CustomerDTO>>> GetCustomersAsync()
        {
            var response = new ServiceResponse<List<CustomerDTO>>();

            try
            {
                var customers = _customerRepository.GetAll().ToList();

                response.Data = customers.Select(c => new CustomerDTO
                {
                    Id = c.Id,
                    BusinessName = c.BusinessName,
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