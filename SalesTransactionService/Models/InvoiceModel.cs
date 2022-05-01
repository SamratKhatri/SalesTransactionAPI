using System.Collections.Generic;

namespace SalesTransactionService.Models
{
    public class InvoiceModel
    {
        public int InvoiceId { get; set; }
        public string InvoiceDate { get; set; }
        public decimal InvoiceTotal { get; set; }
        public int CustomerIdFK { get; set; }
        public List<int> SalesTransactionIds { get; set; }
    }
}