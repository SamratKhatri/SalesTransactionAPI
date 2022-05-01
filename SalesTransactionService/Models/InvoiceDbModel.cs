namespace SalesTransactionService.Models
{
    public class InvoiceDbModel
    {
        public string Id { get; set; }
        public string CustomerName { get; set; }
        public string InvoiceDate { get; set; }
        public string Total { get; set; }
        public string ProductName { get; set; }
        public string Rate { get; set; }
        public string Quantity { get; set; }
        public string IndividualTotal { get; set; }
    }
}