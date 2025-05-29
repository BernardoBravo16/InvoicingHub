using InvoicingHub.Domain.Shared;

namespace InvoicingHub.Domain.Entities
{
    public class CustomerType : IGenericEntity<int>
    {
        public int Id { get; set; }
        public string Type { get; set; } = default!;

        public ICollection<Customer>? Customers { get; set; }
    }
}