using InvoicingHub.Domain.Shared;

namespace InvoicingHub.Domain.Entities
{
    public class Product : IGenericEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public byte[]? Image { get; set; }
        public decimal UnitPrice { get; set; }
        public string? Extension { get; set; }

        public ICollection<InvoiceDetail>? InvoiceDetails { get; set; }
    }
}