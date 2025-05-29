namespace InvoicingHub.Application.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Image { get; set; }
        public decimal UnitPrice { get; set; }
    }
}