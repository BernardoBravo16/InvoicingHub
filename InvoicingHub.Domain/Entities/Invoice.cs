using InvoicingHub.Domain.Shared;

namespace InvoicingHub.Domain.Entities
{
    public class Invoice : IGenericEntity<int>
    {
        public int Id { get; set; }
        public DateTime IssuedDate { get; set; }
        public int CustomerId { get; set; }
        public int InvoiceNumber { get; set; }
        public int TotalItems { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Taxes { get; set; }
        public decimal Total { get; set; }

        public Customer? Customer { get; set; }
        public ICollection<InvoiceDetail>? Details { get; set; }
    }
}