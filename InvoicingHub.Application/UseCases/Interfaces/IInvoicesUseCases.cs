using InvoicingHub.Application.DTO;
using InvoicingHub.Application.Shared.Contracts.Responses;

namespace InvoicingHub.Application.UseCases.Interfaces
{
    public interface IInvoicesUseCases
    {
        Task<ServiceResponse<int>> Create(CreateInvoiceCommand command);
        Task<ServiceResponse<InvoiceModel>> GetInvoice(string invoiceNumber);
        Task<ServiceResponse<List<InvoiceSummaryModel>>> GetInvoiceByCustomer(int customerId);
    }
}