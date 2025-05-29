using InvoicingHub.Domain.Shared;

namespace InvoicingHub.Domain.Entities
{
    public class InvoiceDetail : IGenericEntity<int>
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Subtotal { get; set; }
        public string? Notes { get; set; }

        public Invoice? Invoice { get; set; }
        public Product? Product { get; set; }
    }
}