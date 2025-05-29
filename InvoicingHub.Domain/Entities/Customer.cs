using InvoicingHub.Domain.Shared;

namespace InvoicingHub.Domain.Entities
{
    public class Customer : IGenericEntity<int>
    {
        public int Id { get; set; }
        public string BusinessName { get; set; } = default!;
        public int CustomerTypeId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string RFC { get; set; } = default!;

        public CustomerType? CustomerType { get; set; }
        public ICollection<Invoice>? Invoices { get; set; }
    }
}
