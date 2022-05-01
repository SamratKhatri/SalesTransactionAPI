using System.Collections.Generic;

namespace SalesTransactionService.Models
{
    public class InvoiceWithSalesTransactionModel
    {
        public string Id { get; set; }
        public string CustomerName { get; set; }
        public string InvoiceDate { get; set; }
        public string Total { get; set; }

        public List<SalesTransactionDataModel> SalesTransactionDetail {
            get;
            set;
        }
    }

    public class SalesTransactionDataModel
    {
        public string ProductName { get; set; }
        public string Rate { get; set; }
        public string Quantity { get; set; }
        public string Total { get; set; }
    }
}