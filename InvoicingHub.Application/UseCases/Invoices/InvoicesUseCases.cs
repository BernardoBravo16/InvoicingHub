using InvoicingHub.Application.DTO;
using InvoicingHub.Application.Shared.Contracts.Persistence;
using InvoicingHub.Application.Shared.Contracts.Responses;
using InvoicingHub.Application.UseCases.Interfaces;
using InvoicingHub.Domain.Entities;
using System.Net;

namespace InvoicingHub.Application.UseCases.Invoices
{
    public class InvoicesUseCases : IInvoicesUseCases
    {
        private readonly IRepository<Invoice, int> _invoiceRepository;
        private readonly IRepository<InvoiceDetail, int> _detailRepository;
        private readonly IRepository<Customer, int> _customerRepository;
        private readonly IRepository<Product, int> _productRepository;
        private readonly IUnitOfWork _unitOfWork;


        public InvoicesUseCases(IRepository<Invoice, int> invoiceRepository, IRepository<InvoiceDetail, int> detailRepository, IRepository<Customer, int> customerRepository, IRepository<Product, int> productRepository, IUnitOfWork unitOfWork)
        {
            _invoiceRepository = invoiceRepository;
            _detailRepository = detailRepository;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResponse<int>> Create(CreateInvoiceCommand command)
        {
            var response = new ServiceResponse<int>();

            try
            {
                var exists = await _invoiceRepository.FirstOrDefaultAsync(i => i.InvoiceNumber.Equals(command.InvoiceNumber));

                if (exists != null)
                {
                    response.SetFaultyState(HttpStatusCode.BadRequest, "Invoice number already exists.");
                    return response;
                }

                decimal subtotal = command.Details.Sum(d => d.UnitPrice * d.Quantity);
                decimal tax = Math.Round(subtotal * 0.19m, 2);
                decimal total = subtotal + tax;

                var invoice = new Invoice
                {
                    InvoiceNumber = command.InvoiceNumber,
                    CustomerId = command.CustomerId,
                    IssuedDate = DateTime.Now,
                    Subtotal = subtotal,
                    Taxes = tax,
                    Total = total,
                    TotalItems = command.Details.Count
                };

                await _invoiceRepository.AddAsync(invoice); 
                await _unitOfWork.SaveAsync();              

                foreach (var item in command.Details)
                {
                    var detail = new InvoiceDetail
                    {
                        InvoiceId = invoice.Id,
                        ProductId = item.ProductId,
                        UnitPrice = item.UnitPrice,
                        Quantity = item.Quantity,
                        Subtotal = item.UnitPrice * item.Quantity,
                        Notes = item.Notes
                    };

                    await _detailRepository.AddAsync(detail);
                }

                await _unitOfWork.SaveAsync();

                response.Data = invoice.Id;
                return response;
            }
            catch (Exception ex)
            {
                response.SetFaultyState(HttpStatusCode.InternalServerError, ex);
                return response;
            }

        }

        public async Task<ServiceResponse<InvoiceModel>> GetInvoice(string invoiceNumber)
       
        {
            var response = new ServiceResponse<InvoiceModel>();

            try
            {
                var invoice = await _invoiceRepository.FirstOrDefaultAsync(i => i.InvoiceNumber == int.Parse(invoiceNumber));
                if (invoice == null)
                {
                    response.SetFaultyState(HttpStatusCode.NotFound, "Invoice not found.");
                    return response;
                }

                var details = _detailRepository
                         .GetAll()
                         .Where(d => d.InvoiceId == invoice.Id)
                         .ToList();

                response.Data = new InvoiceModel
                {
                    InvoiceNumber = invoice.InvoiceNumber,
                    CustomerId = invoice.CustomerId,
                    Date = invoice.IssuedDate,
                    Subtotal = invoice.Subtotal,
                    Tax = invoice.Taxes,
                    Total = invoice.Total,
                    Details = details.Select(d => new InvoiceDetailModel
                    {
                        ProductId = d.ProductId,
                        UnitPrice = d.UnitPrice,
                        Quantity = d.Quantity,
                        Total = d.Subtotal,
                        Notes = d.Notes
                    }).ToList()
                };

                return response;
            }
            catch (Exception ex)
            {
                response.SetFaultyState(HttpStatusCode.InternalServerError, ex);
                return response;
            }
        }

        public async Task<ServiceResponse<List<InvoiceSummaryModel>>> GetInvoiceByCustomer(int customerId)
        {
            var response = new ServiceResponse<List<InvoiceSummaryModel>>();

            try
            {
                var invoices = _invoiceRepository
                    .GetAll()
                    .Where(i => i.CustomerId == customerId)
                    .ToList();

                response.Data = invoices.Select(i => new InvoiceSummaryModel
                {
                    InvoiceNumber = i.InvoiceNumber,
                    Date = i.IssuedDate,
                    Total = i.Total
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