namespace SalesTransactionService.Models
{
    public class SalesTransactionModel
    {
        public string SalesTransactionId { get; set; }
        public int CustomerIdFK { get; set; }
        public int ProductIdFK { get; set; }
        public decimal Rate { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        public int InvoiceId { get; set; }
    }
}