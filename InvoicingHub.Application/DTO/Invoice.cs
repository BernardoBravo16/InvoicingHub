namespace InvoicingHub.Application.DTO
{
    public class CreateInvoiceCommand
    {
        public int InvoiceNumber { get; set; }
        public int CustomerId { get; set; }
        public List<InvoiceDetailCommand> Details { get; set; }
    }

    public class InvoiceDetailCommand
    {
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string? Notes { get; set; } 
    }

    public class InvoiceModel
    {
        public int InvoiceNumber { get; set; }
        public int CustomerId { get; set; }
        public DateTime Date { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
        public List<InvoiceDetailModel> Details { get; set; }
    }

    public class InvoiceDetailModel
    {
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        public string? Notes { get; set; }
    }

    public class InvoiceSummaryModel
    {
        public int InvoiceNumber { get; set; }
        public DateTime Date { get; set; }
        public decimal Total { get; set; }
    }
}